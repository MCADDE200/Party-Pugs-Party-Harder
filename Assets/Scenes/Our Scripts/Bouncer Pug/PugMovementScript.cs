using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PugMovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RotatePug();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RotatePug()
    {
        this.transform.Rotate(Vector3.up, -90);
    }
}
