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

	public float stillTime = 10;

	public Transform finalVinePos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			t += Time.deltaTime * timeMultiplier * currentDirection;
			transform.localEulerAngles = Vector3.forward * Mathf.Sin (t) * angleMagnitude;
		} else if (t != 0) {
			t += Time.deltaTime * timeMultiplier * currentDirection;
			float magnitude = angleMagnitude * Mathf.InverseLerp (releaseTime + stillTime, releaseTime, Time.time);
			transform.localEulerAngles = Vector3.forward * Mathf.Sin (t) * magnitude;
			if (magnitude == 0) {
				t = 0;
			}
		}
	}

	public Vector2 GetReleaseForce(){
		float direction = Mathf.Sign (Mathf.Cos (t)) * currentDirection;

		float intensity = Mathf.InverseLerp (-1, 1, Mathf.Sin (t));
		if (direction == -1) {
			intensity = 1 - intensity;
		}
		running = false;
		releaseTime = Time.time;

		return transform.right * intensity * maxReleaseForce * direction;
	}

	void OnTriggerEnter2D(Collider2D other){
		Gorilla gorilla = other.GetComponent<Gorilla> ();
		if (gorilla != null && gorilla.SetOnVine (this)) {
			running = true;
			currentDirection = gorilla.FacingRight ? 1 : -1;
			t = 0;
		}
	}
}
