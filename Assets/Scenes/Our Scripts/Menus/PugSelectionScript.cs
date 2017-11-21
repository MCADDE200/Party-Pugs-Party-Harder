using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lean.Touch
{
    public class PugSelectionScript : MonoBehaviour
    {
        public GameObject[] pugSkins;
        public GameObject selectButton;
        int index;
        public int selectedSkin;

        //GameDataScript gameDataScript;

        // Use this for initialization
        void Start()
        {
            index = 7;
            selectedSkin = 2;
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
                if (index == 0)
                {
                    index = 10;
                }
                else
                {
                    index--;
                }
            }

            if (swipe.x > Mathf.Abs(swipe.y))
            {
                if (index == 10)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {
            for (int i = 0; i < 11; i++)
            {
                pugSkins[i].SetActive(false);
            }
            pugSkins[index].SetActive(true);

            
        }
        
        public void selectPug()
        {
            selectedSkin = index;
        }

        public void back()
        {
            SceneManager.LoadScene("Shop");
        }

        protected virtual void OnDisable()
        {
            // Unhook the events
            LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        }
    }
}
