using UnityEngine;
using System.Collections;

public class CrazyBall : MonoBehaviour {
	
	public float maxTimer = 2;
	public Transform CrazyEffect;
	public float child = 3;
	
	void Start () {
		transform.localScale = new Vector3(child, child, child);
		Invoke("Bomb", maxTimer);
	}
	
	private void Bomb(){
		Transform e = (Transform)Instantiate((Transform)CrazyEffect, transform.position, Quaternion.identity);
		e.GetComponent<CrazyEffect>().SetSize(child);
		if(1<child){
			Transform newball1 = (Transform)Instantiate((Transform)transform, transform.position + transform.forward * (child/2), Quaternion.identity);
			Transform newball2 = (Transform)Instantiate((Transform)transform, transform.position - transform.forward * (child/2), Quaternion.identity);
			newball1.GetComponent<CrazyBall>().child = child-1;
			newball2.GetComponent<CrazyBall>().child = child-1;
			newball1.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity + transform.forward * (child/2);
			newball2.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity - transform.forward * (child/2);
		}
		Destroy(gameObject);
	}
}
