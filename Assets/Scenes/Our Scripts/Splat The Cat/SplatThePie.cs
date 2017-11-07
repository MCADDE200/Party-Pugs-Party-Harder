using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplatThePie : MonoBehaviour {

    public GameObject Spawn;
    Rigidbody rb;
    float timer = 60;
    public int life = 3;
    int score = 0;
    public Text Timer, Life, Score;
    public Image GameOverImage, scoreImg;
    public GameObject PlayAgainButton, MainMenuButton;
    public bool gameDone;
    public AudioSource audioSource;
    public AudioClip pieHit, pieMiss, gameOver, wrongSound;
    RespawnHandler respawnScript;

    // Use this for initialization
    void Start ()
    {
        //timer = 60;
        life = 5;
        score = 0;
        rb = GetComponent<Rigidbody>();
        PlayAgainButton.SetActive(false);
        MainMenuButton.SetActive(false);
        GameOverImage.enabled = false;
        scoreImg.enabled = false;
        gameDone = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        respawnScript = gameObject.GetComponent<RespawnHandler>();
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

}
