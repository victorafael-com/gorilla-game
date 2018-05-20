using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float speed = 2;
	public float lifespam = 5;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (lifespam);
		Destroy (gameObject);
	}

	void Update(){
		transform.position += Vector3.left * speed * Time.deltaTime;
	}

	void OnTriggerEnter2D (Collider2D other) {
		var gorilla = other.GetComponent<Gorilla> ();
		if (gorilla != null) {
			gorilla.Die ();
			Destroy (gameObject);
		}
	}
}
