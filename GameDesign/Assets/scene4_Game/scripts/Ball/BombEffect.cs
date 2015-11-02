using UnityEngine;
using System.Collections;

public class BombEffect : MonoBehaviour {

	public GameObject collider;

	void Start () {
		Destroy(collider, 0.05f);
		Destroy(gameObject, 0.3f);
	}
}
