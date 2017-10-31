using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplatThePie : MonoBehaviour {

    public GameObject Spawn;
    Rigidbody rb;
    float timer = 60;
    int life = 3;
    int score = 0;
    public Text Timer, Life, Score;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(Timer.text);
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Timer.text = "Time Left: " + timer.ToString();
        }

        if (timer <= 0 || life <= 0)
        {
            // Splat the cat ends here
        }

        //Just for debug - have to remove it later
        if (transform.position.y < 0)
        {
            gameObject.transform.position = Spawn.transform.position;
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
            score++;
            Score.text = "Score: " + score.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
        }

        if (other.gameObject.name == "Pug")
        {
            life--;
            Life.text = "Lifes left: " + life.ToString();
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.transform.position = Spawn.transform.position;
        }
    }
}
