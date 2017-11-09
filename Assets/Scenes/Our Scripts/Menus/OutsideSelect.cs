using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutsideSelect : MonoBehaviour

{
    public GameObject bouncerPugUI;
    public GameObject carnivalStallsUI;

    private void Start()
    {
        bouncerPugUI.SetActive(false);
        carnivalStallsUI.SetActive(false);
    }

    public void LoadBouncerPugConfirmation()
    {
        bouncerPugUI.SetActive(true);
    }

    public void LoadCarnivalStallConfirmation()
    {
        carnivalStallsUI.SetActive(true);
    }

    public void LoadBouncerPug()
    {
        SceneManager.LoadScene("Bouncer Pug");
    }

    public void LoadCarnivalStalls()
    {
        SceneManager.LoadScene("Carnival Stalls");
    }

    public void CancelUI()
    {
        bouncerPugUI.SetActive(false);
        carnivalStallsUI.SetActive(false);
    }

}