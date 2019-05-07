using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//Constants
	const int SIZEOBJECTIVE=45;
	const float MIN_X_COORDINATE = -4.5f;
	const float MAX_X_COORDINATE = 6.5f;
	const float MIN_Y_COORDINATE = -2.0f;
	const float MAX_Y_COORDINATE = 0.60f;

	//Time
	float timeLimit;
	float timer;

	//Scores
	int playerOneScore;
	int playerTwoScore;
	int playerThreeScore;
	int playerFourScore;

	//Random position
	GameObject[] ballPositions;
	const float RANDOMRADIOUS = 2.5f;

	//Controllers
	bool playingMatch=true;

	//ScoreLabels
	public Text playerOneLabel;
	public Text playerTwoLabel;
	public Text playerThreeLabel;
	public Text playerFourLabel;
	public Text timeLabel;
	public Text finishLabel;

    //Sound
    AudioSource pito;

	void Start () {
		//Initialize Timers
		//timer = 0;
		timeLimit = 210;

		//Get the balls
		ballPositions = GameObject.FindGameObjectsWithTag("Ball");

		//Initialize Scores
		playerOneScore=playerFourScore=playerThreeScore=playerTwoScore=0;

		//Hide Text
		finishLabel.enabled=false;

        pito = GetComponent<AudioSource>();
	}

	void Update (){
		if (playingMatch)
			RunTimer(timeLimit);
	}

	void RunTimer(float timeLimit){
		timer += Time.deltaTime;
        timeLimit -= timer;
		//To Show hundreths of seconds
        if (timeLimit < 10)
            timeLabel.text = timeLimit.ToString().Substring(0, 4);
		else
            timeLabel.text = timeLimit.ToString().Substring(0, 5);
        if (timeLimit <= 0.05)
        {
			EndMatch ();

		}	

		//Test to see if Add Score Works
		//addToScore(Random.Range(1,5),2);
	}


	//Adds a Score and Prints it
	public void addToScore(int player, int points){
		switch (player) 
		{
		case 1:
			playerOneScore += points;
			playerOneLabel.text = playerOneScore.ToString ();
			break;
		case 2:
			playerTwoScore += points;
			playerTwoLabel.text = playerTwoScore.ToString ();
			break;
		case 3:
			playerThreeScore += points;
			playerThreeLabel.text = playerThreeScore.ToString ();
			break;
		case 4:
			playerFourScore += points;
			playerFourLabel.text = playerFourScore.ToString ();
			break;
		}

	}

	void EndMatch(){
		playingMatch = false;

		finishLabel.enabled = true;
        pito.Play();

        Invoke("ToMenu", 10f);

		//showWinner();
	}

    void ToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

	public bool isPlaying(){

		return playingMatch;
	}

	public Vector2 RandomPosition() 
	{
		Vector2 position = new Vector2(Random.Range(MIN_X_COORDINATE, MAX_X_COORDINATE), Random.Range(MIN_Y_COORDINATE, MAX_Y_COORDINATE));
		while (!PositionIsCorrect(position)) 
		{
			position = new Vector2(Random.Range(MIN_X_COORDINATE, MAX_X_COORDINATE), Random.Range(MIN_Y_COORDINATE, MAX_Y_COORDINATE));
		}
		return position;

	}

	bool PositionIsCorrect (Vector2 position)
	{
		Vector2 vectorDistance = new Vector2 ();
		float distance = 0;
		ballPositions = GameObject.FindGameObjectsWithTag("Ball");
		foreach (GameObject ballPosition in ballPositions) {
			vectorDistance = (Vector2) ballPosition.transform.position - position;
			distance = vectorDistance.magnitude;
			if (distance < RANDOMRADIOUS)
				return false;
		}
		print ("New X Position " + distance);	
		return true;
	}


}