using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChestSpread : MonoBehaviour {
	public float scalePerSeconds = 3;
	public float maxScale = 8;
	private float scale;
	private bool spreading = true;
	[SerializeField] private SpriteRenderer _renderer;
	// Use this for initialization
	void Start () {
		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(scale < maxScale && spreading){
			scale += Time.deltaTime * scalePerSeconds;
			transform.localScale = Vector3.one * scale;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		switch (other.tag) {
		case "Enemy":
			other.GetComponent<Hunter> ().Fear ();
			break;
		case "Banana":
			other.GetComponent<Banana> ().Drop ();
			break;
		}
	}
	public void StopSpreading(){
		spreading = false;
		transform.parent = null;
		StartCoroutine (FadeOut ());
	}
	IEnumerator FadeOut(){
		float a = _renderer.color.a;
		float fadeOutTime = 0.25f;
		float lerp = 0;
		do {
			lerp += Time.deltaTime;
			_renderer.color = new Color(1,1,1, Mathf.Lerp(a, 0, lerp));
			yield return null;
		} while(lerp < 1);
		Destroy(gameObject);
	}
}
