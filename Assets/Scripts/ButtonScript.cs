using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

	public string scene;

	public void OnClick()
	{
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
 	