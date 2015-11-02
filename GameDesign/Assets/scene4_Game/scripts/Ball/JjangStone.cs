using UnityEngine;
using System.Collections;

public class JjangStone : MonoBehaviour {
	
	public float maxTimer = 2;
	public Transform bombEffect;
	
	void Start () {
		Invoke("Bomb", maxTimer);
	}
	
	private void Bomb(){
		Instantiate((Transform)bombEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
