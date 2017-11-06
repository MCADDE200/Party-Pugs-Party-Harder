using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatTheCat : MonoBehaviour {

    public GameObject pie;
    float respawnTime = 5;
    SplatThePie pieScript;

	// Use this for initialization
	void Start () {

        gameObject.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
        pieScript = pie.GetComponent<SplatThePie>();
    }
	
	// Update is called once per frame
	void Update () {
        respawnTime -= Time.deltaTime;

        if (respawnTime <= 0)
        {
            gameObject.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
            respawnTime = 5;
            if (this.gameObject.name == "Cat")
            {
                pieScript.life -= 1;
                pieScript.Life.text = "Lifes left: " + pieScript.life.ToString();
            }
        }

	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == pie)
        {
            gameObject.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
        }
    }
}
