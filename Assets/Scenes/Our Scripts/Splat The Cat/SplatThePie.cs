﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplatThePie : MonoBehaviour {

    public GameObject Spawn;
    Rigidbody rb;
    float timer = 60;
    public int life = 3;
    int score = 0;
    public Text Timer, Life, Score;
    public Image GameOverImage;
    public GameObject PlayAgainButton, MainMenuButton;
    public bool gameDone;
    public AudioSource audioSource;
    public AudioClip pieHit, pieMiss, gameOver, wrongSound;

    // Use this for initialization
    void Start ()
    {
        //timer = 60;
        life = 3;
        score = 0;
        rb = GetComponent<Rigidbody>();
        PlayAgainButton.SetActive(false);
        MainMenuButton.SetActive(false);
        GameOverImage.enabled = false;
        gameDone = false;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameDone)
        {
            //if (timer > 0)
            //{
            //    timer -= Time.deltaTime;
            //    Timer.text = "Time Left: " + (int)timer;
            //}

            if (life <= 0)
            {
                audioSource.PlayOneShot(gameOver, 1);
                PlayAgainButton.SetActive(true);
                MainMenuButton.SetActive(true);
                GameOverImage.enabled = true;
                gameDone = true;
            }

            //Just for debug - have to remove it later
            if (transform.position.y < 0)
            {
                gameObject.transform.position = Spawn.transform.position;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Ground")
        {
            rb.velocity = new Vector3(0,0,0);
            gameObject.transform.position = Spawn.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cat")
        {
            audioSource.PlayOneShot(pieHit, 1);
            score++;
            Score.text = "Score: " + score.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
        }

        if (other.gameObject.name == "Pug")
        {
            audioSource.PlayOneShot(pieMiss, 1);
            life--;
            Life.text = "Lifes left: " + life.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
        }
    }

    public void LoadMainMenu()
    {
        Application.LoadLevel("Main Menu");
    }

    public void ResetLevel()
    {
        Application.LoadLevel("Splat The Cat");
    }
}
