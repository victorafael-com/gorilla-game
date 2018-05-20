using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public float moveSpeed = 2;
	public Transform target;
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		float diff = Mathf.Abs (target.position.x - pos.x);
		pos.x = Mathf.MoveTowards(pos.x, target.position.x, moveSpeed * Time.deltaTime * diff);
		transform.position = pos;
	}
}
