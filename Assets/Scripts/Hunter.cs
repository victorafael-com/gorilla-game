using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {
	public float shotInterval = 4;
	public float fearRecoverTime = 5;
	[Header("Components")]
	[SerializeField] private Rigidbody2D _rigidbody;
	[SerializeField] private Animator _animator;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private Transform _bulletPos;

	private float lastShot;
	private bool fear = false;
	// Use this for initialization
	void Start () {
		lastShot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - shotInterval > lastShot) {
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
}
