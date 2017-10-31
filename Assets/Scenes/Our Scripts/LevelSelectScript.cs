using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadBouncerPug()
    {
        Application.LoadLevel("Bouncer Pug");
    }

    public void LoadSplatTheCat()
    {
        Application.LoadLevel("Splat The Cat");
    }
}
