using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovementScript : MonoBehaviour {


    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        //RunForestRun();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 5);
    }

    void RunForestRun()
    {
        
    }
}
