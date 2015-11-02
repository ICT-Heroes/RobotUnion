using UnityEngine;
using System.Collections;

public class AI_Meca_Bullet : MonoBehaviour {
	
	void Start () {
		GetComponent<Rigidbody>().AddForce(transform.forward*10000);
	}

	void Update () {

	}
}
