using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadGame()
    {
        Application.LoadLevel("Level Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
