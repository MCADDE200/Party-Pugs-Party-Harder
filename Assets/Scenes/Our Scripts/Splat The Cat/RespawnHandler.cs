using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnHandler : MonoBehaviour {

    public GameObject cat, pug;
    SplatThePie pieScript;
    public float respawnTime = 5;
    public Text respawnText;

    // Use this for initialization
    void Start ()
    {
        pieScript = gameObject.GetComponent<SplatThePie>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!pieScript.gameDone)
        {
            respawnTime -= Time.deltaTime;
            respawnText.text = "C/D: " + (int)respawnTime;
            if (respawnTime <= 0)
            {
                RespawnBoth();
                respawnTime = 5;
                pieScript.life -= 1;
                pieScript.Life.text = "Lifes left: " + pieScript.life.ToString();

            }
        }
    }

    public void RespawnBoth()
    {
        pug.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
        cat.transform.position = new Vector3(Random.Range(-1, 6), Random.Range(1, 2), Random.Range(7, 11));
    }
}
