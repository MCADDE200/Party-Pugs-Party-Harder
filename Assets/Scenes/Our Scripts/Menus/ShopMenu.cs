using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    
    }
    public void LoadTickets()
    {
        SceneManager.LoadScene("TicketsMenu");
    }

    public void LoadPugs()
    {
        SceneManager.LoadScene("Pug Selection Scene");
    }

    public void LoadPowerUpScene()
    {
        SceneManager.LoadScene("PowerUps");
    }
}
