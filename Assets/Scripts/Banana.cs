using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour {
	[SerializeField] private Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (Mathf.Sign (Random.Range (-1f, 1f)), 1, 1);
		transform.eulerAngles = Vector3.forward * (Random.Range (0, 4) * 90);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Drop(){
		_rigidBody.isKinematic = false;
		_rigidBody.velocity = Vector3.up;
	}

	void OnTriggerEnter2D(Collider2D other){
		var gorilla = other.GetComponent<Gorilla> ();
		if (gorilla != null) {
			Destroy (gameObject);
		}
	}
}
