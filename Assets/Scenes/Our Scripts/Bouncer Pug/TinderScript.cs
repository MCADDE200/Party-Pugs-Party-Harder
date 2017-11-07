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
        
        public GameObject pugEntry;
        public GameObject fakeCat;
        public GameObject resetLevelButton;
        public GameObject mainMenuButton;

        public Text healthText;
        public Text scoreText;
        public Text timerText;

        public Image pos100;
        public Image neg100;
        public Image gameOverImg;

        float countdownTimer;

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
            pos100.enabled = false;
            neg100.enabled = false;
            gameOverImg.enabled = false;
            counter = 0;
            score = 0;
            wrongAnswers = 0;
            gameOver = false;
            countdownTimer = 4f;
            pugCounter = 0;
            catCounter = 0;
            resetLevelButton.SetActive(false);
            mainMenuButton.SetActive(false);
            chooseAnimal();
        }

        // Update is called once per frame
        void Update()
        {
            timerText.text = "Time: " + (int)countdownTimer;
            if (countdownTimer > 1 && (!gameOver))
            {
                countdownTimer -= Time.deltaTime;
            }
            
            //if(countdownTimer <= 0)
            //{
            //    Debug.Log("Time Out");
            //}
            if (wrongAnswers == 3 || (countdownTimer <= 0))
            {
                gameOver = true;
                gameOverImg.enabled = true;
                resetLevelButton.SetActive(true);
                mainMenuButton.SetActive(true);
            }
            healthText.text = "Health: " + (3 - wrongAnswers);
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

        public void ResetLevel()
        {
            Start();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
