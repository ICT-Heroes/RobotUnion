using UnityEngine;
using System.Collections;

public class PartOfBody : MonoBehaviour {

	public int hp = 100;

	public Material whiteColor;
	private Material myMat;
	private int hit = 0;

	// Use this for initialization
	void Start () {
		myMat = transform.GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if(0 < hit){
			hit--;
			if(hit == 0){
				transform.GetComponent<MeshRenderer>().material = myMat;
			}
		}
	}

	public void Damage(int dam){
		transform.GetComponent<MeshRenderer>().material = whiteColor;
		hit = 2;
		hp -= dam;
		if(hp<0){
			Destroy(gameObject);
		}
	}
}
