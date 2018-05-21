using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gorilla : MonoBehaviour {
	public event UnityAction onDie;
	#region Inspector Fields
	[Header("Behaviour")]
	public float movementSpeed;
	public float jumpSpeed;
	public float dieY;
	
	[Header("Components")]
	[SerializeField] private Rigidbody2D _rigidBody;
	[SerializeField] private CapsuleCollider2D _capsuleCollider;
	[SerializeField] private Animator _animator;
	[SerializeField] private Controls _controls;
	[SerializeField] private Transform _vinePosition;
	[SerializeField] private GameObject _hitChestPrefab;
	[SerializeField] private Transform _hitChestPosition;
	[SerializeField] private LayerMask _movementRaycastMask;
	[SerializeField] private LayerMask _enemyRaycastMask;
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
	private bool canMove = true;
	private bool dead;
	private Vine currentVine;
	private HitChestSpread currentHitChest;


	// Use this for initialization
	void Start () {
		FacingRight = true;
	}
	
	void Update () {
		if (dead)
			return;
		
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
		} else if (currentVine != null) {
			currentVine = null; //Allows gorilla to hold again on the same vine if he hit the ground
			canMove = true;
		}

		if (canMove) {
			velocity.x = _controls.inputX * movementSpeed;
			Vector2 pos = _capsuleCollider.offset + Vector2.up * 0.1f;
			if (velocity.x < 0)
				pos.x *= -1;

			if (Mathf.Abs (velocity.x) > 0) {
				var result = Physics2D.CapsuleCast (transform.TransformPoint (pos), _capsuleCollider.size, _capsuleCollider.direction, 0, Vector2.right * velocity.x,0.2f, _movementRaycastMask);
				if (result.collider != null && !result.collider.isTrigger) {
					velocity.x = 0;
				}
			}
		}

		if ((velocity.x < 0 && FacingRight) || (velocity.x > 0 && !FacingRight)) {
			FacingRight = !FacingRight;
			transform.localScale = new Vector3 (FacingRight ? 1 : -1, 1, 1);
		}

		_animator.SetBool ("moving", velocity.x != 0);


		if (transform.position.y < dieY) {
			dead = true;
			velocity.x = 0;
			if (onDie != null)
				onDie ();
		}

		_rigidBody.velocity = velocity;
	}
	public void UpdateVine(){
		float finalPos = currentVine.finalVinePos.localPosition.y - _vinePosition.localPosition.y;
		float newPos = Mathf.MoveTowards (transform.localPosition.y, finalPos, Time.deltaTime * 2);
		transform.localPosition = new Vector3 (transform.localPosition.x, newPos);
	}


	public bool SetOnVine(Vine v){
		if (currentVine == v || onVine) {
			return false;
		}

		transform.parent = v.transform;
		transform.localEulerAngles = Vector3.zero;
		Vector3 pos = transform.localPosition;
		pos.x = -_vinePosition.localPosition.x * (FacingRight ? 1 : -1);
		transform.localPosition = pos;

		_rigidBody.isKinematic = true;
		_rigidBody.velocity = Vector2.zero;

		currentVine = v;
		onVine = true;

		_animator.SetBool ("vine", true);

		return true;
	}

	public void JumpPressed(){
		if (Grounded && !onVine) {
			_rigidBody.velocity = new Vector2 (_rigidBody.velocity.x, jumpSpeed);
		} else if (onVine) {
			onVine = false;
			canMove = false;
			transform.parent = null;
			transform.eulerAngles = Vector2.zero;
			_animator.SetBool ("vine", false);
			_rigidBody.isKinematic = false;
			_rigidBody.velocity = currentVine.GetReleaseForce ();
		}
	}
	public void Attack(){
		if (!dead && currentHitChest == null) {
			_animator.SetTrigger ("attack");

			Vector2 pos = _capsuleCollider.offset + Vector2.up * 0.1f;
			if (!FacingRight)
				pos.x *= -1;
			var result = Physics2D.CapsuleCast (transform.TransformPoint (pos), _capsuleCollider.size, _capsuleCollider.direction, 0, Vector2.right * (FacingRight ? 1 : -1),1f, _enemyRaycastMask);
			if (result.collider != null) {
				result.collider.GetComponent<Hunter> ().Die ();
			}

		}
	}
	public void HitChest(bool state){
		if (Grounded) {
			_animator.SetBool ("hitChest", state);
			if (state) {
				currentHitChest = Instantiate<GameObject> (_hitChestPrefab).GetComponent<HitChestSpread>();
				currentHitChest.transform.SetParent (_hitChestPosition, false);
				canMove = false;
			} else if(currentHitChest != null){
				canMove = true;
				currentHitChest.StopSpreading ();
				currentHitChest = null;
			}
		}
	}
	public void Die(){
		if (onVine) {
			JumpPressed ();
		}
		if (currentHitChest != null) {
			currentHitChest.StopSpreading ();
		}
		_animator.SetBool ("dead", true);
		dead = true;

		if (onDie != null)
			onDie ();
	}
}
