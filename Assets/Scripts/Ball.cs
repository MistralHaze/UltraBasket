using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public bool Alive;
	public bool Shot;
	private bool firstCollision;
	bool isRespawning;

	public int score;



    //SOUND SOURCES
    public AudioSource backboard;
    public AudioSource metalRing;

	GameObject gameController;
    AudioSource bote;


	//Initial ball coordinates
	public float BallInitialX;
	public float BallInitialY; 


	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("Controller");
		firstCollision = true;
		isRespawning = true;
		Alive = true;
		Shot = false;
		BallInitialX = transform.position.x;
		BallInitialY = transform.position.y;
        bote = this.GetComponent<AudioSource>();
		//print ("x" + BallInitialX + " y" + BallInitialY);
	}



	public bool isAlive(){
		return Alive;
	}

	public void setDead(){
		Alive = false;
	}

	public bool isShot(){
		return Shot;
	}

	public void setShot(){
		Shot = true;
	}

	//Coliision detection
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Floor" && firstCollision) {
			firstCollision = false;
			Invoke ("restartBall", 1.5f);
		}

        if (coll.gameObject.tag == "Floor")
            bote.Play();
        if (coll.gameObject.tag == "Backboard")
            backboard.Play();
        if (coll.gameObject.tag == "MetalRing")
            metalRing.Play();
	}

	//Restart ball and stop gravity of the ball
	void restartBall(){
		Vector2 newPosition = gameController.GetComponent<GameManager> ().RandomPosition ();
		this.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
		Shot = false;
		firstCollision = true;
		this.GetComponentInParent<ThrowBall> ().stopGravity ();
		BallInitialX = this.transform.position.x;
		BallInitialY = this.transform.position.y;
		this.GetComponentInParent<ThrowBall> ().updateProportions ();
		isRespawning = true;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		
		if (isRespawning) {
			if (other.gameObject.tag == "Zone1") {
                score = 10;
			}
			if (other.gameObject.tag == "Zone2") {
                score = 20;
			}
			if (other.gameObject.tag == "Zone3") {
                score = 30;
			}
		}
        
		isRespawning = false;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Net")
        {
            gameController.GetComponent<GameManager>().addToScore(this.GetComponentInParent<ThrowBall>().PlayerNumber, score);
        }
    }

}
