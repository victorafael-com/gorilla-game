using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Transform end;
	public Transform[] enemyPositions;
	public Rect[] BananaSpawnPos;

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

	public void Setup(Config config){
		int enemySlots = enemyPositions.Length;
		if(enemySlots > 0){
			Transform first = RandomItem<Transform> (enemyPositions);
			if (Random.value < config.firstEnemyChance + config.chanceIncrease * transform.position.x) {
				
				Instantiate (RandomItem<GameObject> (config.enemyPrefabs), first.position, Quaternion.identity);
			}

			if (enemySlots > 1) {
				Transform second;
				do {
					second = RandomItem<Transform> (enemyPositions);
				} while(second == first);

				if (Random.value < config.secondEnemyChance + config.chanceIncrease * transform.position.x) {
					Instantiate (RandomItem<GameObject> (config.enemyPrefabs), second.position, Quaternion.identity);
				}
			}
		}
	}

	private T RandomItem<T>(T[] arr){
		return arr [Random.Range (0, arr.Length)];
	}
}
