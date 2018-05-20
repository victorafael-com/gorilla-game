using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Transform end;
	public Transform[] enemyPositions;
	public Rect BananaSpawnPos;

	// Use this for initialization
	void Start () {
		if (enemyPositions.Length > 0) {

		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		foreach (var t in enemyPositions) {
			Gizmos.DrawSphere (t.position, 0.2f);
		}
	}
}
