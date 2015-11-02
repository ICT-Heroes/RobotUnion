using UnityEngine;
using System.Collections;

public class Arraw : MonoBehaviour {

	private float power;
	private bool isInit = false;
	private Vector3 exPos;
	// Use this for initialization
	void Start () {
		exPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(isInit){
			transform.LookAt(2*transform.position - exPos);
			exPos = transform.position;
		}
	}
	


	public void Init(float p){
		isInit = true;
		power = p;
		transform.GetComponent<Rigidbody>().velocity = transform.forward * p * 10;
	}

	void OnTriggerEnter(Collider coll){
		if(coll.name != "Player"){
			Transform model = transform.FindChild("model");
			model.parent = null;
			model.parent = coll.transform.gameObject.transform;
			model.transform.localScale = new Vector3(1/coll.transform.localScale.x, 1/coll.transform.localScale.y, 1/coll.transform.localScale.z);
			Destroy(gameObject);
		}
	}
}
