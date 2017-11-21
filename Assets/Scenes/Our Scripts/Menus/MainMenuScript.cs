using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GameObject musicData = GameObject.Find("MusicObject");
        if (musicData == null)
        {
            musicData = new GameObject("MusicObject");
            musicData.AddComponent<AudioSource>();
            musicData.AddComponent<MusicScript>();
        }

        GameObject gameData = GameObject.Find("GameData");
        if (gameData == null)
        {
            gameData = new GameObject("GameData");
            gameData.AddComponent<GameDataScript>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadGame()
    {
        SceneManager.LoadScene("Outside");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");

    }
}
