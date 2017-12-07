using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TicketMenuScript : MonoBehaviour
{
    int tickets;
    public Text ticketText;

	// Use this for initialization
	void Start ()
    {
        tickets = 0;
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
    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void AddTickets1()
    {

        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameData.GetComponent<GameDataScript>().tickets += 10;
        }
    }
    public void AddTickets2()
    {
        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameData.GetComponent<GameDataScript>().tickets += 50;
        }

    }
    public void AddTickets3()
    {
        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameData.GetComponent<GameDataScript>().tickets += 100;
        }

    }
    public void AddTickets4()
    {
        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameData.GetComponent<GameDataScript>().tickets += 250;
        }

    }
    public void AddTickets5()
    {
        GameObject gameData = GameObject.Find("GameData");
        if (gameData != null)
        {
            gameData.GetComponent<GameDataScript>().tickets += 500;
        }
    }
}
