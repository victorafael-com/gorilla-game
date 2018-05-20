using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
	public float inputX;

	private Gorilla gorilla;

	[Header("Buttons")]
	public string jump;
	public string attack;
	public string hitChest;

	void Start(){
		gorilla = GetComponent<Gorilla> ();
	}
	// Update is called once per frame
	void Update () {
		inputX = Input.GetAxis ("Horizontal");
		if (Input.GetButtonDown (jump)) {
			gorilla.JumpPressed ();
		}
		if (Input.GetButtonDown (attack)) {
			gorilla.Attack ();
		}
		if (Input.GetButtonDown (hitChest)) {
			gorilla.HitChest (true);
		}
		if (Input.GetButtonUp (hitChest)) {
			gorilla.HitChest (false);
		}
	}
}
