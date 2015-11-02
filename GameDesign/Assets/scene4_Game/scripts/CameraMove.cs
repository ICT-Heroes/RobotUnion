using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameObject player = GameObject.Find("Player");
		transform.parent = player.transform;
		transform.forward = player.transform.forward;
		transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		transform.parent.Rotate(mouseX * new Vector3(0, 100, 0) * Time.deltaTime);
		transform.localEulerAngles -= new Vector3(mouseY * Time.deltaTime * 100, 0, 0);
	}

}
