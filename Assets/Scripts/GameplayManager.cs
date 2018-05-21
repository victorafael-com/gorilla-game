using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
	public Gorilla gorilla;
	public float deathHudDelay = 1.5f;

	public GameObject joystickCanvas;
	public GameObject endGameCanvas;
	// Use this for initialization
	void Start () {
		gorilla.onDie += OnGorillaDie;
	}

	void OnGorillaDie ()
	{
		joystickCanvas.SetActive (false);
		Invoke ("ShowEndGame", deathHudDelay);
	}

	void ShowEndGame(){
		endGameCanvas.SetActive (true);
	}
	// Update is called once per frame
	public void Restart() {
		SceneManager.LoadScene("test");
	}
}
