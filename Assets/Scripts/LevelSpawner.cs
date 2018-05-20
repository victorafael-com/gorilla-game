using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour {
	public Gorilla gorilla;

	public Platform firstPlatform;
	public GameObject[] platformPrefabs;

	public float platformDistance = 2;
	public float createPlatformDistance = 20;

	private List<Platform> platforms;
	private Platform lastPlatform;

	// Use this for initialization
	void Start () {
		lastPlatform = firstPlatform;
		platforms = new List<Platform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gorilla.transform.position.x + createPlatformDistance > lastPlatform.end.position.x) {
			GenerateNextPlatform ();
		}
		if (gorilla.transform.position.x - createPlatformDistance > platforms [0].end.position.x) {
			Destroy (platforms [0].gameObject);
			platforms.RemoveAt (0);
		}
	}

	void GenerateNextPlatform(){
		var prefab = platformPrefabs [Random.Range (0, platformPrefabs.Length)];
		lastPlatform = Instantiate<GameObject> (prefab, lastPlatform.end.position + Vector3.right * platformDistance, Quaternion.identity).GetComponent<Platform>();
		platforms.Add (lastPlatform);
	}
}
