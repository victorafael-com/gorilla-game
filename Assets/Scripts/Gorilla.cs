﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorilla : MonoBehaviour {
	#region Inspector Fields
	[Header("Behaviour")]
	public float movementSpeed;
	public float jumpSpeed;
	
	[Header("Components")]
	[SerializeField] private Rigidbody2D _rigidBody;
	[SerializeField] private Animator _animator;
	[SerializeField] private Controls _controls;
	[SerializeField] private Transform _vinePosition;
	#endregion

	private bool __grounded;
	public bool Grounded{
		get{
			return __grounded;
		}
		set{
			if (__grounded != value) {
				__grounded = value;
				_animator.SetBool ("grounded", value);
			}
		}
	}
	public bool FacingRight{ get; private set; }

	private bool onVine = false;
	private Vine currentVine;


	// Use this for initialization
	void Start () {
		FacingRight = true;
	}
	
	void Update () {
		if (!onVine) {
			UpdateControls ();
		} else {
			UpdateVine ();
		}
	}

	private void UpdateControls(){
		Vector2 velocity = _rigidBody.velocity;
		float ySpeed = velocity.y;
		Grounded = ySpeed == 0;

		if (!Grounded) {
			_animator.SetFloat ("ySpeed", ySpeed);
		}

		velocity.x = _controls.inputX * movementSpeed;
		if ((velocity.x < 0 && FacingRight) || (velocity.x > 0 && !FacingRight)) {
			FacingRight = !FacingRight;
			transform.localScale = new Vector3 (FacingRight ? 1 : -1, 1, 1);
		}

		_animator.SetBool ("moving", velocity.x != 0);

		_rigidBody.velocity = velocity;
	}
	public void UpdateVine(){
		onVine = true;
	}


	public void SetOnVine(Vine v){
		transform.parent = v.transform;
		transform.localEulerAngles = Vector3.zero;
		Vector3 pos = transform.localPosition;
		pos.x = -_vinePosition.localPosition.x;
		transform.localPosition = pos;

		_rigidBody.isKinematic = true;
		_rigidBody.velocity = Vector2.zero;

		currentVine = v;
		onVine = true;

		_animator.SetBool ("vine", true);
	}

	public void JumpPressed(){
		if (Grounded && !onVine) {
			_rigidBody.velocity = new Vector2 (_rigidBody.velocity.x, jumpSpeed);
		} else if (onVine) {

		}
	}
	public void Attack(){
		_animator.SetTrigger ("attack");
	}
	public void HitChest(bool state){
		if (Grounded) {
			_animator.SetBool ("hitChest", state);
		}
	}
}
