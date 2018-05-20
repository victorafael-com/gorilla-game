using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Config{
	public float firstEnemyChance = 0.45f;
	public float secondEnemyChance = 0.05f;

	public float chanceIncrease = 0.002f;

	public int maxBanana = 4;
	public float maxBananaIncrement = 50;
	public float bananaChance = 0.4f;

	public GameObject[] enemyPrefabs;
	public GameObject bananaPrefab;
}
