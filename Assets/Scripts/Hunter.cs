using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour {
	public float shotInterval = 4;
	[Header("Components")]
	[SerializeField] private Rigidbody2D _rigidbody;
	[SerializeField] private Animator _animator;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private Transform _bulletPos;

	private float lastShot;
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
}
