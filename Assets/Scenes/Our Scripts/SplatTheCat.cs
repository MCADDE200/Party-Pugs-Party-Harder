using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatTheCat : MonoBehaviour {

    public GameObject pie;

	// Use this for initialization
	void Start () {

        gameObject.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == pie)
        {
            gameObject.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
        }
    }
}
