using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Lean.Touch
{

    public class TinderScript : MonoBehaviour
    {
        int counter;
        int score;
        int wrongAnswers;
        int pugCounter, catCounter;

        bool pug;
        bool gameOver;
        bool gameStarted;
        bool lerp;
        int state;
        bool leftRightLerp;
        public bool bouncerPaused;
        public bool holdFinalLerpBool;

        List<bool> isPug = new List<bool>();
        
        public GameObject pugEntry;
        public GameObject fakeCat;
        public GameObject resetLevelButton;
        public GameObject mainMenuButton;
        public GameObject pauseButton;
        public GameObject pos;
        //TESTING ONLY 
        public GameObject rightPos;
        public GameObject leftPos;

        GameObject pugTest;
        GameObject catTest;

        public GameObject[] pugSkins;
        public GameObject[] pugArray;
        public GameObject[] catArray;

        public Text scoreText;

        public Image pos100;
        public Image neg100;
        public Image gameOverImg;
        public Image num0Image, num1Image, num2Image, num3Image, goImage, startNum1Image, startNum2Image, startNum3Image; //Stores all the countdown timer images to activate and deactivate
        public Image h1Image, h2Image, h3Image;

        float countdownTimer;
        float startTimer;


        //HAVE TO SWITCH TO LISTS!!!
        Vector3[] newPosPug = new Vector3[150];
        Vector3[] newPosCat = new Vector3[150];
        public int lerpSpeed = 6;

        AudioSource sound;
        Animator anim;

        public AudioClip correctSound;
        public AudioClip correctSound2;
        public AudioClip correctSound3;

        public AudioClip wrongSound;
        public AudioClip wrongSound2;
        public AudioClip wrongSound3;

        //POWER UPS
        int goldenBoneCount;
        int crossPugCount;
        int goldenBoneScoreMultiplier;
        bool goldenBone;
        bool crossPug;
        float powerUpTimer; //how long before timer gets deactivated

        private void Awake()
        {
            sound = GetComponent<AudioSource>();
        }
        // Use this for initialization
        void Start()
        {
            GameObject gameData = GameObject.Find("GameData");
            goldenBoneCount = gameData.GetComponent<GameDataScript>().goldenBone;
            crossPugCount = gameData.GetComponent<GameDataScript>().crossPug;

            powerUpTimer = 0;
            goldenBone = false;
            crossPug = false;
            goldenBoneScoreMultiplier = 1;
            num0Image.enabled = false;
            num1Image.enabled = false;
            num2Image.enabled = false;
            num3Image.enabled = false;
            goImage.enabled = false;
            startNum1Image.enabled = false;
            startNum2Image.enabled = false;
            startNum3Image.enabled = true;
            h1Image.enabled = true;
            h2Image.enabled = true;
            h3Image.enabled = true;
            pos100.enabled = false;
            neg100.enabled = false;
            gameOverImg.enabled = false;

            counter = 0;
            score = 0;
            wrongAnswers = 0;

            gameOver = false;
            gameStarted = false;

            countdownTimer = 4f;
            startTimer = 4f;
            pugCounter = 0;
            catCounter = 0;

            bouncerPaused = false;
            resetLevelButton.SetActive(false);
            mainMenuButton.SetActive(false);
            pauseButton.SetActive(true);

            for (int i = 0; i < 10; i++)
            {
                isPug.Add(false);
            }

            populateList();
            SpawnQueue();
            lerp = false;
            //SpawnAnimal();

            //for (int i = 0; i < 11; i++)
            //{
            //    pugSkins[i].SetActive(false);
            //}
            //GameObject gameData = GameObject.Find("GameData");
            //if (gameData != null)
            //{
            //    pugSkins[gameData.GetComponent<GameDataScript>().selectedSkin].SetActive(true) ;
            //}
            ////pugSkins[gameDataScript.selectedSkin].SetActive(true);

            ////chooseAnimal();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameStarted)
            {
                if (countdownTimer > 1 && (!gameOver) && !crossPug)
                {
                    countdownTimer -= Time.deltaTime;
                }

                if (countdownTimer > 3)
                {
                    num3Image.enabled = true;
                }
                else if (countdownTimer > 2)
                {
                    num3Image.enabled = false;
                    num2Image.enabled = true;
                }
                else if (countdownTimer > 1)
                {
                    num2Image.enabled = false;
                    num1Image.enabled = true;
                }
                else
                {
                    num1Image.enabled = false;
                    num0Image.enabled = true;
                }
                if (lerp)
                {
                    MoveForward();
                    AnimatePugs(state);
                }

                //include all future power ups here!
                if (powerUpTimer <= 0)
                {
                    goldenBone = false;
                    goldenBoneScoreMultiplier = 1;
                    crossPug = false;
                }
                else
                {
                    powerUpTimer -= Time.deltaTime;
                }
                
            }
            if (!gameStarted)
            {
                if (startTimer > 0 && (!gameOver))
                {
                    startTimer -= Time.deltaTime;
                }
                if (startTimer > 3)
                {
                    startNum3Image.enabled = true;
                }
                else if (startTimer > 2)
                {
                    startNum3Image.enabled = false;
                    startNum2Image.enabled = true;
                }
                else if (startTimer > 1)
                {
                    startNum2Image.enabled = false;
                    startNum1Image.enabled = true;
                }
                else
                {
                    startNum1Image.enabled = false;
                    StartCoroutine(goImagePopup());
                    gameStarted = true;
                }
            }
            
            switch(wrongAnswers)
            {
                case 1:
                    h3Image.enabled = false;
                    break;
                case 2:
                    h2Image.enabled = false;
                    break;
            }
            
            if (wrongAnswers == 3 || (countdownTimer <= 1))
            {
                gameOver = true;
                gameOverImg.enabled = true;
                h1Image.enabled = false;
                h2Image.enabled = false;
                h3Image.enabled = false;
                resetLevelButton.SetActive(true);
                mainMenuButton.SetActive(true);
            }
            scoreText.text = "Score: " + score;

            if(leftRightLerp)
            {
                //Debug.Log("tuka");
                LerpFinal(holdFinalLerpBool);
            }
        }

        public void GoldenBone()
        {
            GameObject gameData = GameObject.Find("GameData");
            gameData.GetComponent<GameDataScript>().goldenBone = goldenBoneCount;
            if (goldenBoneCount > 0)
            {
                goldenBone = true;
                goldenBoneScoreMultiplier = 2;
                goldenBoneCount -= 1;
                gameData.GetComponent<GameDataScript>().goldenBone -= 1;
                powerUpTimer = 5;
            }
            else
            {
                Debug.Log("insufficient power ups");
            }
        }

        public void CrossPug()
        {
            GameObject gameData = GameObject.Find("GameData");
            gameData.GetComponent<GameDataScript>().crossPug = crossPugCount;
            if (crossPugCount > 0)
            {
                crossPug = true;
                crossPugCount -= 1;
                gameData.GetComponent<GameDataScript>().crossPug -= 1;
                powerUpTimer = 5;
            }
            else
            {
                Debug.Log("insufficient power ups");
            }
        }

        void SpawnAnimal()
        {
            //Destroy(GameObject.Find("PugTest" + (counter - 1)));
            //Destroy(GameObject.Find("CatTest" + (counter - 1)));
            isPug.RemoveAt(0);
            leftRightLerp = true;

            //MoveForwards();
            //LerpPos();
            //LerpRight();

            ChooseAnimal();

            if (isPug[9] == true)
            {
                GameObject pugTest = Instantiate(pugArray[Random.Range(0, 40)], pos.transform);
                pugTest.transform.position = new Vector3(pugTest.transform.position.x, pugTest.transform.position.y, pugTest.transform.position.z - 9);
                pugTest.name = "PugTest" + (counter + 9);
            }
            else
            {
                GameObject catTest = Instantiate(catArray[Random.Range(0, 5)], pos.transform);
                catTest.transform.position = new Vector3(catTest.transform.position.x, catTest.transform.position.y, catTest.transform.position.z - 9);
                catTest.name = "CatTest" + (counter + 9);
            }
            LerpPos();
        }

        void SpawnQueue()
        {
            for(int i = 0; i < isPug.Count; i++)
            {
                if (isPug[i] == true)
                {
                    GameObject pugTest = Instantiate(pugArray[Random.Range(0, 40)], pos.transform);
                    pugTest.transform.position = new Vector3(pugTest.transform.position.x, pugTest.transform.position.y, pugTest.transform.position.z - i);
                    pugTest.name = "PugTest" + i;
                }
                else
                {
                    GameObject catTest = Instantiate(catArray[Random.Range(0, 5)], pos.transform);
                    catTest.transform.position = new Vector3(catTest.transform.position.x, catTest.transform.position.y, catTest.transform.position.z - i);
                    catTest.name = "CatTest" + i;
                }
            }
        }

        //void MoveForwards()
        //{
        //    for(int i = 0; i < (isPug.Count + 1); i++)
        //    {
        //        int offset = i + counter;
        //        if(GameObject.Find("PugTest" + offset) != null && !pugDone)
        //        {
        //            GameObject temp = GameObject.Find("PugTest" + offset);
        //            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.position.z + 1);
        //        }
        //        if (GameObject.Find("CatTest" + offset) != null && !catDone)
        //        {
        //            GameObject temp = GameObject.Find("CatTest" + offset);
        //            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, temp.transform.position.z + 1);

        //        }
        //    }
        //}

        void LerpPos()
        {
            for (int i = 0; i < (isPug.Count - 1); i++)
            {
                int offset = i + counter;
                if (GameObject.Find("PugTest" + offset) != null)
                {
                    GameObject temp = GameObject.Find("PugTest" + offset);
                    newPosPug[offset] = new Vector3(temp.transform.position.x, temp.transform.position.y, Mathf.Ceil(temp.transform.position.z + 1));
                }
                if (GameObject.Find("CatTest" + offset) != null)
                {
                    GameObject temp = GameObject.Find("CatTest" + offset);
                    newPosCat[offset] = new Vector3(temp.transform.position.x, temp.transform.position.y, Mathf.Ceil(temp.transform.position.z + 1));
                }
            }
            state = 1;
            //AnimatePugs(state);
            lerp = true;
            //MoveForward();
            
        }

        void MoveForward()
        {
            for (int i = 0; i < (isPug.Count - 1); i++)
            {
                int offset = i + counter;
                if (GameObject.Find("PugTest" + offset) != null)
                {
                    GameObject temp = GameObject.Find("PugTest" + offset);
                    if (temp.transform.position != newPosPug[offset])
                    {
                        temp.transform.position = Vector3.Lerp(temp.transform.position, newPosPug[offset], Time.deltaTime * lerpSpeed);
                        if(temp.transform.position == newPosPug[offset])
                        {
                            lerp = false;
                            state = 2;
                            AnimatePugs(state);
                        }
                    }
                }
                if (GameObject.Find("CatTest" + offset) != null)
                {
                    GameObject temp = GameObject.Find("CatTest" + offset);
                    if (temp.transform.position != newPosCat[offset])
                    {
                        temp.transform.position = Vector3.Lerp(temp.transform.position, newPosCat[offset], Time.deltaTime * lerpSpeed);
                        if (temp.transform.position == newPosCat[offset])
                        {
                            lerp = false;
                            state = 2;
                            AnimatePugs(state);
                        }
                    }
                }
            }
            //lerp = false;
            //state = 2;
            //AnimatePugs(state);
        }

        void AnimatePugs(int state)
        {

            if (state == 1)
            {
                for (int i = 0; i < (isPug.Count - 1); i++)
                {
                    int offset = i + counter;
                    if (GameObject.Find("PugTest" + offset) != null)
                    {
                        GameObject temp = GameObject.Find("PugTest" + offset);
                        anim = temp.GetComponentInChildren<Animator>();
                        anim.SetBool("Walking", true);
                    }
                    if (GameObject.Find("CatTest" + offset) != null)
                    {
                        GameObject temp = GameObject.Find("CatTest" + offset);
                        anim = temp.GetComponentInChildren<Animator>();
                        anim.SetBool("Walking", true);
                    }
                }
            }
            if(state == 2)
            {
                for (int i = 0; i < (isPug.Count - 1); i++)
                {
                    int offset = i + counter;
                    if (GameObject.Find("PugTest" + offset) != null)
                    {
                        GameObject temp = GameObject.Find("PugTest" + offset);
                        anim = temp.GetComponentInChildren<Animator>();
                        anim.SetBool("Walking", false);
                    }
                    if (GameObject.Find("CatTest" + offset) != null)
                    {
                        GameObject temp = GameObject.Find("CatTest" + offset);
                        anim = temp.GetComponentInChildren<Animator>();
                        anim.SetBool("Walking", false);
                    }
                }
            }
            //state = 0;
        }

        void LerpFinal(bool dir)
        {
            if (dir)
            {
                if (GameObject.Find("PugTest" + (counter - 1)) != null)
                {
                    GameObject temp = GameObject.Find("PugTest" + (counter-1));
                    temp.transform.position = Vector3.Lerp(temp.transform.position, rightPos.transform.position, Time.deltaTime * lerpSpeed);
                    //Debug.Log("PugTest" + (counter-1));
                    if (temp.transform.position == rightPos.transform.position)
                    {
                        //Debug.Log("trqq go disablene");
                        leftRightLerp = false;
                        //Destroy(temp);
                    }
                }
                if (GameObject.Find("CatTest" + (counter - 1)) != null)
                {
                    GameObject temp = GameObject.Find("CatTest" + (counter-1));
                    temp.transform.position = Vector3.Lerp(temp.transform.position, rightPos.transform.position, Time.deltaTime * lerpSpeed);
                    //Debug.Log("CatTest" + (counter-1));
                    if (temp.transform.position == rightPos.transform.position)
                    {
                        //Debug.Log("trqq go disablene");
                        leftRightLerp = false;
                        //Destroy(temp);
                    }
                }

            }
            else if(!dir)
            {
                if (GameObject.Find("PugTest" + (counter - 1)) != null)
                {
                    GameObject temp = GameObject.Find("PugTest" + (counter-1));
                    temp.transform.position = Vector3.Lerp(temp.transform.position, leftPos.transform.position, Time.deltaTime * lerpSpeed);
                    //Debug.Log("PugTest" + (counter-1));
                    if (temp.transform.position == leftPos.transform.position)
                    {
                        leftRightLerp = false;
                        //Destroy(temp);
                    }
                }
                if (GameObject.Find("CatTest" + (counter - 1)) != null)
                {
                    GameObject temp = GameObject.Find("CatTest" + (counter-1));
                    temp.transform.position = Vector3.Lerp(temp.transform.position, leftPos.transform.position, Time.deltaTime * lerpSpeed);
                    //Debug.Log("CatTest" + (counter-1));
                    if (temp.transform.position == leftPos.transform.position)
                    {
                        leftRightLerp = false;
                        //Destroy(temp);
                    }
                }
            }
        }

        void ChooseAnimal()
        {
            int animal = Random.Range(1, 11);
            animal += pugCounter;
            animal -= catCounter;
            if (wrongAnswers < 3)
            {
                if (animal < 6)
                {
                    pug = true;
                    isPug.Add(true);
                    catCounter = 0;
                    pugCounter++;
                }
                else
                {
                    pug = false;
                    isPug.Add(false);
                    pugCounter = 0;
                    catCounter++;
                }
            }
        }

        void populateList()
        {
            for (int i = 0; i < 10; i++)
            {
                int animal = Random.Range(1, 11);
                animal += pugCounter;
                animal -= catCounter;
                if (animal < 6)
                {
                    pug = true;
                    isPug[i] = true;
                    catCounter = 0;
                    pugCounter++;
                }
                else
                {
                    pug = false;
                    isPug[i] = false;
                    pugCounter = 0;
                    catCounter++;
                }
            }
            

        }

        protected virtual void OnEnable()
        {
            // Hook into the events we need
            LeanTouch.OnFingerSwipe += OnFingerSwipe;
        }

        protected virtual void OnDisable()
        {
            // Unhook the events
            LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        }

        public void OnFingerSwipe(LeanFinger finger)
        {
            // Make sure the info text exists
            //if (InfoText != null)
            //{
            //    // Store the swipe delta in a temp variable
            if ((!bouncerPaused) && (gameStarted) && (!gameOver))
            {
                var swipe = finger.SwipeScreenDelta;
                if (wrongAnswers < 3)
                {
                    if (swipe.x < -Mathf.Abs(swipe.y))
                    {
                        // Debug.Log("Left!");
                        if (isPug[0] == false)
                        {
                            counter++;
                            SpawnAnimal();
                            bool a = true;
                            StartCoroutine(scorePopup(a));
                            holdFinalLerpBool = false;
                            LerpFinal(isPug[0]);

                        }
                        else
                        {
                            counter++;
                            SpawnAnimal();
                            bool a = false;
                            StartCoroutine(scorePopup(a));
                            holdFinalLerpBool = false;
                            LerpFinal(isPug[0]);
                        }
                    }

                    if (swipe.x > Mathf.Abs(swipe.y))
                    {
                        //Debug.Log("Right!");
                        if (isPug[0] == true)
                        {
                            counter++;
                            SpawnAnimal();
                            bool a = true;
                            StartCoroutine(scorePopup(a));
                            holdFinalLerpBool = true;
                            LerpFinal(isPug[0]);
                        }
                        else
                        {
                            counter++;
                            SpawnAnimal();
                            bool a = false;
                            StartCoroutine(scorePopup(a));
                            holdFinalLerpBool = true;
                            LerpFinal(isPug[0]);
                        }
                    }
                }
                //else
                //{
                //    Debug.Log("Game Over! /n Your Score was: " + score + " You got " + wrongAnswers + " Answers wrong");
                //}

                //if (swipe.y < -Mathf.Abs(swipe.x))
                //{
                //    Debug.Log("Down!");
                //}

                //if (swipe.y > Mathf.Abs(swipe.x))
                //{
                //    Debug.Log("Up!");
                //}
            }
        }

        IEnumerator scorePopup(bool imageShow)
        {
            int soundChoice;
            soundChoice = Random.Range(0, 3);
            if (counter <= 10)
            {
                countdownTimer = 4.0f;
            }
            else if (counter > 10 && counter <= 20)
            {
                countdownTimer = 3.0f;
            }
            else if (counter > 20)
            {
                countdownTimer = 2.0f;
            }
            if (imageShow)
            {
                pos100.enabled = true;
                score += 100 * goldenBoneScoreMultiplier;
                switch(soundChoice)
                {
                    case 0:
                        sound.PlayOneShot(correctSound);
                        break;
                    case 1:
                        sound.PlayOneShot(correctSound2);
                        break;
                    case 2:
                        sound.PlayOneShot(correctSound3);
                        break;
                }
                
            }
            else
            {
                neg100.enabled = true;
                if(!crossPug)
                    score -= 100;
                switch (soundChoice)
                {
                    case 0:
                        sound.PlayOneShot(wrongSound);
                        break;
                    case 1:
                        sound.PlayOneShot(wrongSound2);
                        break;
                    case 2:
                        sound.PlayOneShot(wrongSound3);
                        break;
                }
                wrongAnswers++;
                Handheld.Vibrate();
            }
            yield return new WaitForSeconds(0.5f);
            pos100.enabled = false;
            neg100.enabled = false;  
        }

        IEnumerator goImagePopup()
        {
            goImage.enabled = true;
            yield return new WaitForSeconds(0.3f);
            goImage.enabled = false;
        }

        public void ResetLevel()
        {
            for (int i = 0; i < (isPug.Count + 1); i++)
            {
                int offset = i + counter;
                Destroy(GameObject.Find("PugTest" + offset));
                Destroy(GameObject.Find("CatTest" + offset));
            }
            for (int i = 0; i < 200; i++) // Hardcoded temp fix
            {
                //int offset = i + counter;
                Destroy(GameObject.Find("PugTest" + i));
                Destroy(GameObject.Find("CatTest" + i));
            }
            isPug.Clear();
            Start();
            
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void LoadPauseScene()
        {
            bouncerPaused = true;
            Time.timeScale = 0;
            pauseButton.SetActive(false);
            SceneManager.LoadScene("Pause Scene", LoadSceneMode.Additive);
        }
    }
}
