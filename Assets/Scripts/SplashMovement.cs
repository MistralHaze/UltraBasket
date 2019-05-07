using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<RectTransform>().position = new Vector2 (gameObject.GetComponent<RectTransform>().position.x + Mathf.Cos(Time.frameCount * 0.005f) * 0.055f, gameObject.GetComponent<RectTransform>().position.y);
	}
}
