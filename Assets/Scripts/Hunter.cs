using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {
	public float shotInterval = 4;
	public float fearRecoverTime = 5;
	[Header("Components")]
	[SerializeField] private Rigidbody2D _rigidbody;
	[SerializeField] private Collider2D _collider;
	[SerializeField] private Animator _animator;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private Transform _bulletPos;

	private float lastShot;
	private bool awaken = false;
	private bool fear = false;
	private bool alive = true;
	// Use this for initialization
	void Start () {
	}

	void OnBecameVisible(){
		awaken = true;
		lastShot = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if (alive && awaken && Time.time - shotInterval > lastShot) {
			lastShot = Time.time;
			Instantiate (_bulletPrefab, _bulletPos.position, Quaternion.identity);
		}
	}

	public void Fear(){
		if (fear) { //Only fear once
			return;
		}

		_animator.SetBool ("fear", true);
		fear = true;
		lastShot = Time.time + fearRecoverTime;

		Invoke ("RecoverFromFear", fearRecoverTime);
	}
	void RecoverFromFear(){
		_animator.SetBool ("fear", false);
	}
	public void Die(){
		if (alive) {
			alive = false;
			_animator.SetTrigger ("die");
			_collider.enabled = false;
			_rigidbody.isKinematic = true;
		}
	}
}
