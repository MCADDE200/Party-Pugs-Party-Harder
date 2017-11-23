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
        string currentScene = SceneManager.GetActiveScene().name;

        GameObject fingerSwipe = GameObject.Find("FingerSwipeObject");
        if (fingerSwipe != null)
        {
            
            Lean.Touch.TinderScript tinderScript = fingerSwipe.GetComponent<Lean.Touch.TinderScript>();
            
            if (currentScene == "Bouncer Pug")
            {
                tinderScript.bouncerPaused = false;
                tinderScript.pauseButton.SetActive(true);
            }
            
        }

        GameObject pieTouch = GameObject.Find("PieTouch");
        if(pieTouch != null)
        {
            Lean.Touch.SplatTheCatTouch splatScript = pieTouch.GetComponent<Lean.Touch.SplatTheCatTouch>();

            if (currentScene == "Splat The Cat")
            {
                splatScript.splatTheCatPaused = false;
            }
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
