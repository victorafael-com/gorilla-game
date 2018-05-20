using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {
	public float angleMagnitude = 30;
	public float timeMultiplier = 1;

	public float maxReleaseForce = 4;

	private int currentDirection = 0;
	private float t = 0;
	private float releaseTime;
	private bool running = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			t += Time.deltaTime * timeMultiplier * currentDirection;
			transform.localEulerAngles = Vector3.forward * Mathf.Sin (t) * angleMagnitude;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Gorilla gorilla = other.GetComponent<Gorilla> ();
		if (gorilla != null) {
			gorilla.SetOnVine (this);
			running = true;
			currentDirection = gorilla.FacingRight ? 1 : -1;
		}
	}
}
