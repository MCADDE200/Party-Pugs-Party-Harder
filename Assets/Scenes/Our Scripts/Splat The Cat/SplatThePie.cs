﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplatThePie : MonoBehaviour {

    public GameObject Spawn;
    Rigidbody rb;
    float timer = 60;
    float startTimer;
    public int life = 3;
    int score = 0;
    public Text Timer, Life, Score;
    public Image GameOverImage, scoreImg, heartImg1, heartImg2, heartImg3, heartImg4, heartImg5, startNum1Image, startNum2Image, startNum3Image, goImage;
    public GameObject PlayAgainButton, MainMenuButton, pieTouch;
    public bool gameDone, gameStarted;
    public AudioSource audioSource;
    public AudioClip pieHit, pieMiss, gameOver, wrongSound;
    RespawnHandler respawnScript;


    // Use this for initialization
    void Start ()
    {
        //timer = 60;
        life = 5;
        score = 0;
        startTimer = 3;
        rb = GetComponent<Rigidbody>();
        PlayAgainButton.SetActive(false);
        MainMenuButton.SetActive(false);
        GameOverImage.enabled = false;
        scoreImg.enabled = false;
        gameDone = false;
        goImage.enabled = false;
        startNum1Image.enabled = false;
        startNum2Image.enabled = false;
        startNum3Image.enabled = true;
        gameStarted = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        respawnScript = gameObject.GetComponent<RespawnHandler>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (!gameStarted)
        {
            if (startTimer > 0 && (!gameDone))
            {
                startTimer -= Time.deltaTime;
            }
            if (startTimer > 3)
            {
                startNum3Image.enabled = true;
            }
            else if (startTimer > 2)
            {
                startNum3Image.enabled = false;
                startNum2Image.enabled = true;
            }
            else if (startTimer > 1)
            {
                startNum2Image.enabled = false;
                startNum1Image.enabled = true;
            }
            else
            {
                startNum1Image.enabled = false;
                StartCoroutine(goImagePopup());
                gameStarted = true;
            }
        }


        if (!gameDone && gameStarted)
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

            switch(life)
            {
                case 1:
                    heartImg1.enabled = true;
                    heartImg2.enabled = false;
                    heartImg3.enabled = false;
                    heartImg4.enabled = false;
                    heartImg5.enabled = false;
                    break;
                case 2:
                    heartImg1.enabled = true;
                    heartImg2.enabled = true;
                    heartImg3.enabled = false;
                    heartImg4.enabled = false;
                    heartImg5.enabled = false;
                    break;
                case 3:
                    heartImg1.enabled = true;
                    heartImg2.enabled = true;
                    heartImg3.enabled = true;
                    heartImg4.enabled = false;
                    heartImg5.enabled = false;
                    break;
                case 4:
                    heartImg1.enabled = true;
                    heartImg2.enabled = true;
                    heartImg3.enabled = true;
                    heartImg4.enabled = true;
                    heartImg5.enabled = false;
                    break;
                case 5:
                    heartImg1.enabled = true;
                    heartImg2.enabled = true;
                    heartImg3.enabled = true;
                    heartImg4.enabled = true;
                    heartImg5.enabled = true;
                    break;
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
            respawnScript.respawnTime = 5;
            audioSource.PlayOneShot(pieHit, 1);
            score+=100;
            StartCoroutine(scorePopUp());
            Score.text = "Score: " + score.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
            respawnScript.RespawnBoth();
        }

        if (other.gameObject.name == "Human")
        {
            respawnScript.respawnTime = 5;
            audioSource.PlayOneShot(pieHit, 1);
            score += 200;
            StartCoroutine(scorePopUp2());
            Score.text = "Score: " + score.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
            respawnScript.RespawnBoth();
        }

        if (other.gameObject.name == "Pug")
        {
            respawnScript.respawnTime = 5;
            audioSource.PlayOneShot(pieMiss, 1);
            life--;
            Life.text = "Lifes left: " + life.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
            respawnScript.RespawnBoth();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("Splat The Cat");
    }


    IEnumerator scorePopUp()
    {
        scoreImg.enabled = true;
        yield return new WaitForSeconds(0.5f);
        scoreImg.enabled = false;
        
    }

    IEnumerator scorePopUp2()
    {
        scoreImg.enabled = true;
        yield return new WaitForSeconds(0.2f);
        scoreImg.enabled = false;
        yield return new WaitForSeconds(0.1f);
        scoreImg.enabled = true;
        yield return new WaitForSeconds(0.2f);
        scoreImg.enabled = false;
    }

    IEnumerator goImagePopup()
    {
        goImage.enabled = true;
        yield return new WaitForSeconds(0.3f);
        goImage.enabled = false;
    }

    public void LoadPauseScene()
    {
        gameDone = true;
        Time.timeScale = 0;
        SceneManager.LoadScene("Pause Scene", LoadSceneMode.Additive);
    }
}
