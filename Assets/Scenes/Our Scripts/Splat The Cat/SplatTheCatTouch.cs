using UnityEngine;

namespace Lean.Touch
{
    // This script will push a rigidbody around when you swipe
    [RequireComponent(typeof(Rigidbody))]
    public class SplatTheCatTouch : MonoBehaviour
    {
        // This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
        public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

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
            // Raycast information
            var ray = finger.GetStartRay();
            var hit = default(RaycastHit);

            // Was this finger pressed down on a collider?
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
            {
                // Was that collider this one?
                if (hit.collider.gameObject.name == "PieTouch")
                {
                    // Get the rigidbody component
                    var rigidbody = GameObject.Find("Pie").GetComponent<Rigidbody>();

                    // Add force to the rigidbody based on the swipe force
                    rigidbody.AddForce(finger.SwipeScaledDelta.x * 0.7f, finger.SwipeScaledDelta.y * 0.7f, (Mathf.Abs(finger.SwipeScaledDelta.x * 0.7f) + Mathf.Abs(finger.SwipeScaledDelta.y * 0.7f)) / 2);


                }
            }
        }
    }
}