using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour {

    //Arduino
    string datosArduino;
    int handDistance;
    const int MAX_ARDUINO_DISTANCE = 45; 

	//Number of the player
	public int PlayerNumber;

	//Power Bar
	public Image Bar;

	//Max power
	public const float MAX_VERTICAL_POWER = 15f;
	public const float MAX_HORIZONTAL_POWER = -15f;
    

	//Max and Min X/Y coordinates
	public const float MIN_X_COORDINATE = -5.5f;  //Para el random poner luego -4.5
	public const float MAX_X_COORDINATE = 7.5f;	  //Para el random poner luego 6.5
	public const float MIN_Y_COORDINATE = -2.0f;
	public const float MAX_Y_COORDINATE = 0.60f;

	//Power proportions for the trajectory
	public const float MIN_PROPORTION = 0.8f;
	public const float MAX_PROPORTION = 1.4f;
    float FORCE_DIVIDE = 4.5f;
	float xProportion;
	float yProportion;
    float Y_PROPORTION_MULTIPLIER = 1.6f;
    float X_PROPORTION_MULTIPLIER = 0.7f;

	public GameObject GameController;

	//Vertical and Horizontal power of the ball when realease
	float powerVertical;
	float powerHorizontal;

	public GameObject Ball;
	public Rigidbody2D Rb;

	private bool shooting;
    private bool powerCharging;

	//Key code aux
	private KeyCode key;


	void Start () {

		Ball.GetComponent<Rigidbody2D> ().simulated = false; //Stop gravity
		shooting = false;
        powerCharging = false;
	
		updateProportions ();

		//Change key depending on the player
		switch (PlayerNumber) {

		case 1:
				key = KeyCode.A;
				break;
			case 2:
				key = KeyCode.F;
				break;
			case 3:
				key = KeyCode.J;
				break;
			case 4:
				key = KeyCode.L;
				break;
		}

        handDistance = MAX_ARDUINO_DISTANCE;
	}

	void Update () {

		//print ("propv" + yProportion + " proph" + xProportion);
		//print ("V" + powerVertical + " H" + powerHorizontal);

		bool alive = Ball.GetComponent<Ball> ().isAlive ();
		bool shot = Ball.GetComponent<Ball> ().isShot ();

		//Charge power of the ball
        if (handDistance != MAX_ARDUINO_DISTANCE && !shooting && alive && !shot)
        {

			//InvokeRepeating ("powerCharge", 0f, 0.1f);
            powerCharge();
            powerCharging = true;

		}

		//When key up 
		if (handDistance == MAX_ARDUINO_DISTANCE && powerCharging && !shooting && alive && !shot && GameController.GetComponent<GameManager>().isPlaying()) {

            powerCharging = false;

			Ball.GetComponent<Rigidbody2D> ().simulated = true; 		//Return gravity

			Rb.velocity = new Vector2 (powerHorizontal, powerVertical); //Set power to the ball
			shooting = true;

			CancelInvoke ();
			Invoke ("stopShooting", 0.3f);

			Bar.fillAmount = 0;
		}


	}
		
	//Add power to Horizontar and Vertical power variables
	void powerCharge(){
		
		if (powerVertical < MAX_VERTICAL_POWER && powerHorizontal > MAX_HORIZONTAL_POWER) {

			//Add power and multiply it by the proportions
            print("Shot Distance: "+ handDistance);
			powerVertical = (handDistance / FORCE_DIVIDE )  * yProportion; 
			powerHorizontal = (-handDistance / FORCE_DIVIDE) *X_PROPORTION_MULTIPLIER /* * xProportion*/;
            print("Shot DistanceFINAL: " + powerHorizontal);
			Bar.fillAmount =powerVertical/15 - 0.2f;


		}

	}

	//reset powerVariables
	void stopShooting(){
		shooting = false;
		powerHorizontal = 0;
		powerVertical = 0;
		Ball.GetComponent<Ball> ().setShot ();

	}

	//Stop gravity function to call when the player returns to shoot mode
	public void stopGravity(){
		Ball.GetComponent<Rigidbody2D> ().simulated = false;
	}

    public void setdatosArduino(int datos)
    {
        //print("CANAPÉ" + datos);
        handDistance = datos;
    }

	public void updateProportions(){
		//Esto de aquí se puede mejorar
		//Change the X/Y Power proportions in order to make different trajectories
		//For example: if you are close to the net, you need to shoot upper than if you are far from it.
		float xStep = ((MAX_Y_COORDINATE - MIN_Y_COORDINATE) / 6);
		float yStep = ((MAX_X_COORDINATE - MIN_X_COORDINATE) / 4);
		xProportion = ((Ball.GetComponent<Ball> ().BallInitialY - MIN_Y_COORDINATE) / yStep / 10 + MIN_PROPORTION)* X_PROPORTION_MULTIPLIER;
		yProportion = (MAX_PROPORTION - ((Ball.GetComponent<Ball> ().BallInitialX - MIN_X_COORDINATE) * xStep / 10)) * Y_PROPORTION_MULTIPLIER + (xProportion/10);
        //print("xprop " + xProportion +"->yprop" + yProportion);
	}
		

}
