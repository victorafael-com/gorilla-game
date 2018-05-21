using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	public Transform end;
	public Transform[] enemyPositions;
	public Rect[] bananaSpawnPos;

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
		foreach(var r in bananaSpawnPos){
			Gizmos.DrawWireCube(transform.position + new Vector3(r.center.x, r.center.y), new Vector3(r.size.x, r.size.y));
		}
	}

	public void Setup(Config config){
		int enemySlots = enemyPositions.Length;
		if(enemySlots > 0){
			Transform first = RandomItem<Transform> (enemyPositions);
			if (Random.value < config.firstEnemyChance + config.chanceIncrease * transform.position.x) {
				
				DoInstantiate (RandomItem<GameObject> (config.enemyPrefabs), first.position);
			}

			if (enemySlots > 1) {
				Transform second;
				do {
					second = RandomItem<Transform> (enemyPositions);
				} while(second == first);

				if (Random.value < config.secondEnemyChance + config.chanceIncrease * transform.position.x) {
					DoInstantiate (RandomItem<GameObject> (config.enemyPrefabs), second.position);
				}
			}
		}

		if (bananaSpawnPos.Length > 0 && Random.value < config.bananaChance) {
			int bananaCount = Random.Range (1, config.maxBanana + Mathf.FloorToInt (transform.position.x / config.maxBananaIncrement));
			for (int i = 0; i < bananaCount; i++) {
				Rect r = RandomItem<Rect> (bananaSpawnPos);
				DoInstantiate (config.bananaPrefab, transform.position + new Vector3 (Random.Range (r.xMin, r.xMax), Random.Range (r.yMin, r.yMax)));
			}
		}
	}

	private void DoInstantiate(GameObject prefab, Vector3 position){
		Instantiate <GameObject>(prefab, position, Quaternion.identity).transform.parent = transform;
	}

	private T RandomItem<T>(T[] arr){
		return arr [Random.Range (0, arr.Length)];
	}
}
