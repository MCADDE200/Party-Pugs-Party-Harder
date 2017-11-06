using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplatTheCat : MonoBehaviour {

    public GameObject pie;
    RespawnHandler respawnScript;

	// Use this for initialization
	void Start ()
    {
        gameObject.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
        respawnScript = pie.GetComponent<RespawnHandler>();
    }
	
	// Update is called once per frame
	void Update ()
    {


	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == pie)
        {
            respawnScript.RespawnBoth();
            //respawnScript.respawnTime = 5;
        }
    }
}
