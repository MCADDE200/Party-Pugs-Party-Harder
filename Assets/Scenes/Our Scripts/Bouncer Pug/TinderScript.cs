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
        public bool bouncerPaused;
        
        public GameObject pugEntry;
        public GameObject fakeCat;
        public GameObject resetLevelButton;
        public GameObject mainMenuButton;
        public GameObject pauseButton;

        public Text scoreText;

        public Image pos100;
        public Image neg100;
        public Image gameOverImg;
        public Image num0Image, num1Image, num2Image, num3Image, goImage, startNum1Image, startNum2Image, startNum3Image; //Stores all the countdown timer images to activate and deactivate
        public Image h1Image, h2Image, h3Image;

        float countdownTimer;
        float startTimer;

        AudioSource sound;

        public AudioClip correctSound;
        public AudioClip wrongSound;

        private void Awake()
        {
            sound = GetComponent<AudioSource>();
        }
        // Use this for initialization
        void Start()
        {
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
            chooseAnimal();
        }

        // Update is called once per frame
        void Update()
        {
            if (gameStarted)
            {
                if (countdownTimer > 1 && (!gameOver))
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
            
            //if(countdownTimer <= 0)
            //{
            //    Debug.Log("Time Out");
            //}
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
        }

        void chooseAnimal()
        {

            pugEntry.SetActive(false);
            fakeCat.SetActive(false);
            int animal = Random.Range(1, 11);
            animal += pugCounter;
            animal -= catCounter;
            if (wrongAnswers < 3)
            {
                if (animal < 6)
                {
                    pug = true;
                    //Debug.Log("Pug!");
                    pugEntry.SetActive(true);
                    counter++;
                    catCounter = 0;
                    pugCounter++;
                }
                else
                {
                    pug = false;
                    //Debug.Log("Stupid Cat!");
                    fakeCat.SetActive(true);
                    counter++;
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
                        if (!pug)
                        {
                            chooseAnimal();
                            bool a = true;
                            StartCoroutine(scorePopup(a));
                        }
                        else
                        {
                            chooseAnimal();
                            bool a = false;
                            StartCoroutine(scorePopup(a));
                        }
                    }

                    if (swipe.x > Mathf.Abs(swipe.y))
                    {
                        //Debug.Log("Right!");
                        if (pug)
                        {
                            chooseAnimal();
                            bool a = true;
                            StartCoroutine(scorePopup(a));
                        }
                        else
                        {
                            chooseAnimal();
                            bool a = false;
                            StartCoroutine(scorePopup(a));
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
                score += 100;
                sound.PlayOneShot(correctSound);
            }
            else
            {
                neg100.enabled = true;
                score -= 100;
                sound.PlayOneShot(wrongSound);
                wrongAnswers++;
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
            SceneManager.LoadScene("Pause Scene", LoadSceneMode.Additive);
        }
    }
}
