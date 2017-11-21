using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatNipScript : MonoBehaviour {

    bool pug;

    AudioSource sound;

    public AudioClip Pug_Growl_01;
    public AudioClip Cat_Pug_02;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    //    if 
    //    {
    //        pug = true;
    //        sound.PlayOneShot(Pug_Growl_01);
    //    }

    //    else
    //    {
    //        pug = false;
    //        sound.PlayOneShot(Cat_Pug_02);
    //    }

    }
}
