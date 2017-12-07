using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerUpsScript : MonoBehaviour {


    public Text ticketText;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Performance heavy - needs optimizing for IP3
        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        { 
            ticketText.text = "" + gameData.GetComponent<GameDataScript>().tickets;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GoldenBone()
    {

        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            if (gameData.GetComponent<GameDataScript>().tickets >= 100)
            {
                gameData.GetComponent<GameDataScript>().tickets -= 100;
                gameData.GetComponent<GameDataScript>().goldenBone += 1;
            }
            else
            {
                Debug.Log("u need moar tickets bruh");
            }
        }
    }

    public void CrossPug()
    {
        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            if (gameData.GetComponent<GameDataScript>().tickets >= 100)
            {
                gameData.GetComponent<GameDataScript>().tickets -= 100;
                gameData.GetComponent<GameDataScript>().crossPug += 1;
            }
            else
            {
                Debug.Log("u need moar tickets bruh");
            }
        }
    }

    public void TicketAmount()
    {
        //Output ticket amount
    }
}
