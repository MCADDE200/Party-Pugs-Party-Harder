using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lean.Touch
{

    public class TinderScript : MonoBehaviour
    {
        int counter = 0;
        int numAnimals = 10;
        int score = 0;
        int wrongAnswers = 0;

        bool pug;
        
        public GameObject pugEntry;
        public GameObject fakeCat;

        public Text healthText;
        public Text scoreText;
        public Text timerText;

        float countdownTimer = 30.0f;

        // Use this for initialization
        void Start()
        {
            chooseAnimal();
        }

        // Update is called once per frame
        void Update()
        {
            if (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime;
            }
            timerText.text = "Time: " + (int)countdownTimer; 
            if(countdownTimer <= 0)
            {
                Debug.Log("Time Out");
            }
            healthText.text = "Health: " + (3 - wrongAnswers);
            scoreText.text = "Score: " + score;
        }

        void chooseAnimal()
        {
            pugEntry.SetActive(false);
            fakeCat.SetActive(false);
            int animal = Random.Range(0, 2);
            if(animal == 0)
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
            if (counter < numAnimals && wrongAnswers < 3)
            {
                if (swipe.x < -Mathf.Abs(swipe.y))
                {
                    // Debug.Log("Left!");
                    if (!pug)
                    {
                        score++;
                        if (counter < numAnimals)
                        {
                            chooseAnimal();
                        }
                        else
                        {
                            Debug.Log(score);
                        }
                    }
                    else
                    {
                        wrongAnswers++;
                        if (counter < numAnimals)
                        {
                            chooseAnimal();
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
                        score++;
                        if (counter < numAnimals)
                        {
                            chooseAnimal();
                        }
                        else
                        {
                            Debug.Log(score);
                        }
                    }
                    else
                    {
                        wrongAnswers++;
                        if (counter < numAnimals)
                        {
                            chooseAnimal();
                        }
                        else
                        {

                            Debug.Log(score);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Game Over! /n Your Score was: " + score + " You got " + wrongAnswers + " Answers wrong");
            }

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
}
