using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallSelect : MonoBehaviour

{ 

    public GameObject loadingImage;

    public void LoadScene(int SplatTheCat)
    {
        loadingImage.SetActive(true);
        Application.LoadLevel("Splat The Cat");
    }


}