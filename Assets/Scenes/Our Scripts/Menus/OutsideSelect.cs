using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lean.Touch
{
    public class OutsideSelect : MonoBehaviour

    {
        public GameObject bouncerPugUI;
        public GameObject carnivalStallsUI;
        public GameObject bouncerButton, splatButton, busButton,bouncerInfoButton, splatInfoButton,bouncerPanel,splatPanel,closeButton1,closeButton2,rightButton,leftButton;
       
        int levelSelect;
   

        private void Start()
        {
            bouncerPugUI.SetActive(false);
            carnivalStallsUI.SetActive(false);
            levelSelect = 0;
        }

        private void Update()
        {
            switch(levelSelect)
            {
                case 0:
                    bouncerButton.SetActive(true);
                    splatButton.SetActive(false);
                    bouncerInfoButton.SetActive(true);
                    splatInfoButton.SetActive(false);
                    //busButton.SetActive(false);
                    break;
                case 1:
                    bouncerButton.SetActive(false);
                    splatButton.SetActive(true);
                    bouncerInfoButton.SetActive(false);
                    splatInfoButton.SetActive(true);

                    //busButton.SetActive(false);
                    break;
                case 2:
                    bouncerButton.SetActive(false);
                    splatButton.SetActive(false);
                    bouncerInfoButton.SetActive(false);
                    splatInfoButton.SetActive(false);


                    //busButton.SetActive(true);
                    break;
            }
        }
        protected virtual void OnEnable()
        {
            // Hook into the events we need
            LeanTouch.OnFingerSwipe += OnFingerSwipe;
        }

        public void OnFingerSwipe(LeanFinger finger)
        {
            var swipe = finger.SwipeScreenDelta;

            if (swipe.x < -Mathf.Abs(swipe.y))
            {
                if (levelSelect == 0)
                {
                    levelSelect = 1;
                }
                else
                {
                    levelSelect--;
                }
            }

            if (swipe.x > Mathf.Abs(swipe.y))
            {
                if (levelSelect == 1)
                {
                    levelSelect = 0;
                }
                else
                {
                    levelSelect++;
                }
            }
        }

        public void LoadBouncerPugConfirmation()
        {
            //bouncerPugUI.SetActive(true);
            SceneManager.LoadScene("Bouncer Pug");
        }

        public void LoadCarnivalStallConfirmation()
        {
            SceneManager.LoadScene("Splat The Cat");
            //carnivalStallsUI.SetActive(true);
        }

        public void LoadChaseTheBus()
        {
            SceneManager.LoadScene("Chase The Bus");
            //carnivalStallsUI.SetActive(true);
        }

        public void LoadBouncerPug()
        {
            SceneManager.LoadScene("Bouncer Pug");
        }

        public void LoadCarnivalStalls()
        {
            SceneManager.LoadScene("Carnival Stalls");
        }

        public void CancelUI()
        {
            bouncerPugUI.SetActive(false);
            carnivalStallsUI.SetActive(false);
        }
        public void OpenBouncerInfo()
        {
            bouncerPanel.SetActive(true);
        }
        public void CloseBouncerInfo()
        {
            bouncerPanel.SetActive(false);
        
        }
        public void CloseSplatInfo()
        {
            splatPanel.SetActive(false);

        }
        public void OpenSplatInfo()
        {
            splatPanel.SetActive(true);

        }
        public void ChangeGame()
        {
            if (levelSelect == 0)
            {
                levelSelect = 1;
            }
            else
            {
                levelSelect--;
            }

        }

        protected virtual void OnDisable()
        {
            // Unhook the events
            LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        }

    }
}