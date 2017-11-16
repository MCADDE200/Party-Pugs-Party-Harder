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
        ticketText.text = "Tickets: " + tickets;
		
	}
    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    
    }
    public void AddTickets1()
    {
        tickets += 10;

    }
    public void AddTickets2()
    {
        tickets += 50;

    }
    public void AddTickets3()
    {
        tickets += 100;

    }
    public void AddTickets4()
    {
        tickets += 250;

    }
    public void AddTickets5()
    {
        tickets += 500;

    }
}
