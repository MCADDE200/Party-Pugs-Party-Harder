using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lean.Touch
{
    public class ChaseMovementScript : MonoBehaviour
    {


        Rigidbody rb;

        protected virtual void OnEnable()
        {
            // Hook into the events we need
            LeanTouch.OnFingerSwipe += OnFingerSwipe;
        }

        

        // Use this for initialization
        void Start()
        {
            rb = this.GetComponent<Rigidbody>();
            //RunForestRun();
        }

        // Update is called once per frame
        void Update()
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 5);
        }

        void RunForestRun()
        {

        }

        public void OnFingerSwipe(LeanFinger finger)
        {
            var swipe = finger.SwipeScreenDelta;

            if (swipe.y > Mathf.Abs(swipe.x))
            {
                Debug.Log("Up");
                rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
            }
        }

        protected virtual void OnDisable()
        {
            // Unhook the events
            LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        }
    }
}
