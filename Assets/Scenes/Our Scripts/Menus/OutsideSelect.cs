using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutsideSelect : MonoBehaviour

{ 

    public void LoadBouncerPug()
    {
        SceneManager.LoadScene("Bouncer Pug");
    }

    public void LoadCarnivalStalls()
    {
        SceneManager.LoadScene("Carnival Stalls");
    }

}