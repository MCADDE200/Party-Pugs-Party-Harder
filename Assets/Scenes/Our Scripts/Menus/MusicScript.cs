﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour {

    AudioSource music;

    public AudioClip menuMusic;
    public AudioClip bouncerPugMusic;
    public AudioClip splatTheCatMusic;

    bool menuMusicOn;

    void Awake()
    {
        music = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        menuMusic = Resources.Load("Music/MenuMusic") as AudioClip;
        bouncerPugMusic = Resources.Load("Music/BouncerPugMusic") as AudioClip;
        splatTheCatMusic = Resources.Load("Music/SplatTheCatMusic") as AudioClip;
        music.clip = menuMusic;
        music.Play();
        menuMusicOn = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if((scene.name == "Main Menu" || scene.name == "Level Select" || scene.name == "Outside" || scene.name == "Carnival Stalls" || scene.name == "Shop" || scene.name == "TicketsMenu") && (!menuMusicOn))
        {
            music.clip = menuMusic;
            music.Play();
            menuMusicOn = true;
        }
        if(scene.name == "Bouncer Pug")
        {
            music.clip = bouncerPugMusic;
            music.Play();
            menuMusicOn = false;
        }
        if(scene.name == "Splat The Cat")
        {
            music.clip = splatTheCatMusic;
            music.Play();
            menuMusicOn = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
