using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResumeGame()
    {
        GameObject fingerSwipe = GameObject.Find("FingerSwipeObject");
        if (fingerSwipe != null)
        {
            Lean.Touch.TinderScript tinderScript = fingerSwipe.GetComponent<Lean.Touch.TinderScript>();
            tinderScript.paused = false;
        }
        Destroy(gameObject);
        Time.timeScale = 1;
        
    }

    public void LoadMainMenu()
    {
        Destroy(gameObject);
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
}
