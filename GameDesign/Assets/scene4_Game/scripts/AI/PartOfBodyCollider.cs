using UnityEngine;
using System.Collections;

public class PartOfBodyCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnterMessage(){
		if(transform.GetComponent<PartOfBody>()){
			transform.GetComponent<PartOfBody>().Damage(10);
		}else{
			transform.parent.GetComponent<PartOfBodyCollider>().OnTriggerEnterMessage();
		}
	}

	void OnTriggerEnter(Collider coll){
		OnTriggerEnterMessage();
	}
}
