using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideSelect : MonoBehaviour

{ 

    public GameObject loadingImage;

    public void LoadScene(int BouncerPug)
    {
        loadingImage.SetActive(true);
        Application.LoadLevel("Bouncer Pug");
    }

    
}