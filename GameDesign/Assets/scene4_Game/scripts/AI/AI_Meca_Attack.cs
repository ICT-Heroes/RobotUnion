using UnityEngine;
using System.Collections;

public class AI_Meca_Attack : MonoBehaviour {


	public bool test;
	private GameObject player;
	public Transform bullet;
	public GameObject waist, leftSholder, leftArm0, leftArm1, leftArm2, rightSholder, rightArm0, rightArm1, rightArm2, gun, gunPoint;

	private Vector3 rightArm1Rot, rightArm2Rot;

	void Start () {
		player = GameObject.Find("Player");
		rightArm1Rot = rightArm1.transform.rotation.eulerAngles;
		rightArm2Rot = rightArm2.transform.rotation.eulerAngles;
	}

	void Update () {
		if(test){
			TestAttack();
		}
	}

	private void CreateBullet(){
		Transform newBullet = (Transform)Instantiate(bullet, gunPoint.transform.position, Quaternion.identity);
		newBullet.LookAt(player.transform.position);
	}

	private void TestAttack(){
		if(Input.GetMouseButtonDown(0)){
			waist.transform.localRotation = Quaternion.Euler(new Vector3(0, GetWaistFloatY(), 0));
			rightArm1.transform.rotation = Quaternion.Euler(rightArm1Rot + new Vector3(0, GetArm1FloatY(), 0));
			//rightArm2.transform.rotation = Quaternion.Euler(rightArm2Rot + new Vector3(GetArm2FloatX(), 0, 0));
			//rightArm2.transform.LookAt(player.transform.position);
			CreateBullet();
		}
	}

	private float GetWaistFloatY(){
		Vector3 myRot = transform.forward;
		Vector3 target = (player.transform.position-transform.position);
		myRot = Vector3.Normalize(new Vector3(myRot.x, 0, myRot.z));
		target = Vector3.Normalize(new Vector3(target.x, 0, target.z));
		GameObject gar = new GameObject();
		gar.transform.LookAt(myRot + target);
		float ret = gar.transform.rotation.eulerAngles.y;
		Destroy(gar);
		return ret;
	} 

	private float GetArm1FloatY(){
		Vector3 target = (player.transform.position-transform.position);
		target = Vector3.Normalize(new Vector3(target.x, 0, target.z));
		GameObject gar = new GameObject();
		gar.transform.LookAt(target);
		float ret = gar.transform.rotation.eulerAngles.y;
		Destroy(gar);
		return ret;
	} 

	private float GetArm2FloatX(){
		Vector3 target = (player.transform.position-transform.position);
		target = Vector3.Normalize(new Vector3(0, target.y, target.z));
		GameObject gar = new GameObject();
		gar.transform.LookAt(target);
		float ret = gar.transform.rotation.eulerAngles.x;
		Destroy(gar);
		return ret;
	} 
}
