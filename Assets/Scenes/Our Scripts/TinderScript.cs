using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lean.Touch
{

    public class TinderScript : MonoBehaviour
    {
        int counter;
        int numAnimals = 10;
        int score;
        int wrongAnswers;

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


        // Use this for initialization
        void Start()
        {
            pos100.enabled = false;
            neg100.enabled = false;
            gameOverImg.enabled = false;
            chooseAnimal();
            counter = 0;
            score = 0;
            wrongAnswers = 0;
            gameOver = false;
            countdownTimer = 30.0f;
            resetLevelButton.SetActive(false);
            mainMenuButton.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (countdownTimer > 0 && (!gameOver))
            {
                countdownTimer -= Time.deltaTime;
            }
            timerText.text = "Time: " + (int)countdownTimer; 
            if(countdownTimer <= 0)
            {
                Debug.Log("Time Out");
            }
            if (counter == numAnimals || wrongAnswers == 3)
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
            int animal = Random.Range(0, 2);
            if (counter < numAnimals && wrongAnswers < 3)
            {
                if (animal == 0)
                {
                    pug = true;
                    //Debug.Log("Pug!");
                    pugEntry.SetActive(true);
                    counter++;
                }
                else
                {
                    pug = false;
                    //Debug.Log("Stupid Cat!");
                    fakeCat.SetActive(true);
                    counter++;
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
            if (counter <= numAnimals && wrongAnswers < 3)
            {
                if (swipe.x < -Mathf.Abs(swipe.y))
                {
                    // Debug.Log("Left!");
                    if (!pug)
                    {
                        
                        if (counter <= numAnimals)
                        {
                            chooseAnimal();
                            bool a = true;
                            StartCoroutine(scorePopup(a));
                        }
                        else
                        {
                            Debug.Log(score);
                        }
                    }
                    else
                    {
                        
                        if (counter <= numAnimals)
                        {
                            chooseAnimal();
                            bool a = false;
                            StartCoroutine(scorePopup(a));
                        }
                        else
                        {
                            Debug.Log(score);
                        }
                    }
                }

                if (swipe.x > Mathf.Abs(swipe.y))
                {
                    //Debug.Log("Right!");
                    if (pug)
                    {
                        
                        if (counter <= numAnimals)
                        {
                            chooseAnimal();
                            bool a = true;
                            StartCoroutine(scorePopup(a));
                        }
                        else
                        {
                            Debug.Log(score);
                        }
                    }
                    else
                    {
                        
                        if (counter <= numAnimals)
                        {
                            chooseAnimal();
                            bool a = false;
                            StartCoroutine(scorePopup(a));
                        }
                        else
                        {

                            Debug.Log(score);
                        }
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
            if(imageShow)
            {
                pos100.enabled = true;
                score += 100;
            }
            else
            {
                neg100.enabled = true;
                score -= 100;
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
            Application.LoadLevel("Main Menu");
        }
    }
}
