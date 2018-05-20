using System.Collections;
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

	private bool facingRight = true;
	


	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		Vector2 velocity = _rigidBody.velocity;
		float ySpeed = velocity.y;
		Grounded = ySpeed == 0;

		if (!Grounded) {
			_animator.SetFloat ("ySpeed", ySpeed);
		}

		velocity.x = _controls.inputX * movementSpeed;
		if ((velocity.x < 0 && facingRight) || (velocity.x > 0 && !facingRight)) {
			facingRight = !facingRight;
			transform.localScale = new Vector3 (facingRight ? 1 : -1, 1, 1);
		}

		_animator.SetBool ("moving", velocity.x != 0);

		_rigidBody.velocity = velocity;
	}

	public void JumpPressed(){
		if (Grounded) {
			_rigidBody.velocity = new Vector2 (_rigidBody.velocity.x, jumpSpeed);
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
