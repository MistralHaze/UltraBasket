using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reader : MonoBehaviour {

    const int SENSORPOSITION=2;
    const int DISTANCEPOSITION = 6;

    ArduinoConnector connector;
    string leido;

    public ThrowBall P1throwBallScript;
    public ThrowBall P2throwBallScript;
    public ThrowBall P3throwBallScript;
    public ThrowBall P4throwBallScript;
    int sensorActual;
    int distanciaActual;
    bool parseSensor;
    bool parseDistance;

	// Use this for initialization
	void Start () {
        connector = this.gameObject.AddComponent<ArduinoConnector>();
        connector.Open();
        StartCoroutine(ArduinoLoop());

        

	}
	
    private IEnumerator ArduinoLoop()
    {
        //leido = connector.ReadFromArduino(5000);
        yield return StartCoroutine(connector.AsynchronousReadFromArduino(Print));
        //print(leido); 
	}

    void Print(string leido)
    {
        this.leido=leido;
        //print(leido);
        separaSensoryDistancia(leido);
    }
    void separaSensoryDistancia(string leido) 
    {
        parseSensor = int.TryParse(char.ToString(leido[SENSORPOSITION]), out  sensorActual);
        parseDistance = int.TryParse(leido.Substring(DISTANCEPOSITION), out distanciaActual);

        switch (sensorActual) { 
            case 1:
                P1throwBallScript.setdatosArduino(distanciaActual);
                break;
            case 2:
                P2throwBallScript.setdatosArduino(distanciaActual);
                break;
            case 3:
                P3throwBallScript.setdatosArduino(distanciaActual);
                break;
            case 4:
                P4throwBallScript.setdatosArduino(distanciaActual);
                break;
        }

    }

}
