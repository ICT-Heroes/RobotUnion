using UnityEngine;
using System.Collections;

public class AI_Meca_Move : MonoBehaviour {

	public bool test;

	public float speed = 10;
	public GameObject leftLeg0, leftLeg1, leftFoot, rightLeg0, rightLeg1, rightFoot, pelvis;

	public GameObject[] sparkPos;
	public Transform sparkEffect;
	public Transform smokeEffect;
	private int moveFlag;
	private float animationSpeed = 1;
	private bool rotateRight = false;
	private float rotateTimer = 0;

	private GameObject player;

	void Start () {
		player = GameObject.Find("Player");
	}

	void Update () {

		if(test){
			MoveTest();
		}

		DecisionMove();
		MoveAnimation(5);
		MoveRotate();
	}

	private void DecisionMove(){

	}

	private void Move(int flag){
		switch(flag){
		case MoveFlag.Front:
			transform.GetComponent<Rigidbody>().velocity = transform.forward * 10 * speed;
			break;
		case MoveFlag.Back:
			transform.GetComponent<Rigidbody>().velocity = transform.forward * 10 * (-1) * speed;
			break;
		case MoveFlag.Left:
			transform.GetComponent<Rigidbody>().velocity = transform.right * 10 * (-1) * speed;
			break;
		case MoveFlag.Right:
			transform.GetComponent<Rigidbody>().velocity = transform.right * 10 * speed;
			break;
		case MoveFlag.FrontLeft:
			transform.GetComponent<Rigidbody>().velocity = transform.forward * 7 * speed + transform.right * 7 * speed * (-1);
			break;
		case MoveFlag.FrontRight:
			transform.GetComponent<Rigidbody>().velocity = transform.forward * 7 * speed + transform.right * 7 * speed;
			break;
		case MoveFlag.BackLeft:
			transform.GetComponent<Rigidbody>().velocity = transform.forward * 7 * speed * (-1) + transform.right * 7 * speed * (-1);
			break;
		case MoveFlag.BackRight:
			transform.GetComponent<Rigidbody>().velocity = transform.forward * 7 * speed * (-1) + transform.right * 7 * speed;
			break;
		case MoveFlag.Stay:
			transform.GetComponent<Rigidbody>().velocity = Vector3.Lerp(transform.GetComponent<Rigidbody>().velocity, Vector3.zero, Time.deltaTime);
			break;
		case MoveFlag.RightRotate:
			rotateTimer = 1;
			rotateRight = true;
			break;
		case MoveFlag.LeftRotate:
			rotateTimer = 1;
			rotateRight = false;
			break;
		}
	}

	private void MoveRotate(){
		if(0 < rotateTimer){
			rotateTimer -= Time.deltaTime;
			if(rotateRight){
				Debug.Log((rotateTimer*5)*(rotateTimer*5));
				transform.Rotate(new Vector3(0, (rotateTimer*5)*(rotateTimer*5) * Time.deltaTime * speed, 0));
			}else{
				transform.Rotate(new Vector3(0, (rotateTimer*5)*(rotateTimer*5) * Time.deltaTime * speed, 0) * (-1));
			}
		}
	}

	private void InstantiateEffect(){
		Transform newSpark0 = (Transform)Instantiate(sparkEffect, sparkPos[0].transform.position, sparkPos[0].transform.rotation);
		Transform newSpark1 = (Transform)Instantiate(sparkEffect, sparkPos[1].transform.position, sparkPos[1].transform.rotation);
		Transform newSmoke0 = (Transform)Instantiate(smokeEffect, sparkPos[0].transform.position, sparkPos[0].transform.rotation);
		Transform newSmoke1 = (Transform)Instantiate(smokeEffect, sparkPos[1].transform.position, sparkPos[1].transform.rotation);
		Destroy(newSpark0.gameObject, 0.2f);
		Destroy(newSpark1.gameObject, 0.2f);
		Destroy(newSmoke0.gameObject, 1);
		Destroy(newSmoke1.gameObject, 1);
	}

	public void SetMoveFlag(int flag){
		moveFlag = flag;
		Move(flag);
		if(flag != MoveFlag.Stay){
			InstantiateEffect();
		}

		/*
		switch(flag){
		case MoveFlag.Front:
			break;
		case MoveFlag.Back:
			break;
		case MoveFlag.Left:
			break;
		case MoveFlag.Right:
			break;
		case MoveFlag.FrontLeft:
			break;
		case MoveFlag.FrontRight:
			break;
		case MoveFlag.BackLeft:
			break;
		case MoveFlag.BackRight:
			break;
		case MoveFlag.Stay:
			break;
		case MoveFlag.RightRotate:
			break;
		case MoveFlag.LeftRotate:
			break;
		}*/
	}

	/*
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(00, 00, 00), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(00, 00, 00)), Time.deltaTime * speed);

	 */

	private void MoveTest(){
		if(Input.GetKey("w") && Input.GetKey("a")){
			SetMoveFlag(MoveFlag.FrontLeft);
		}else	if(Input.GetKey("w") && Input.GetKey("d")){
			SetMoveFlag(MoveFlag.FrontRight);
		}else if(Input.GetKey("s") && Input.GetKey("a")){
			SetMoveFlag(MoveFlag.BackLeft);
		}else if(Input.GetKey("s") && Input.GetKey("d")){
			SetMoveFlag(MoveFlag.BackRight);
		}else if(Input.GetKey("w")){
			SetMoveFlag(MoveFlag.Front);
		}else	if(Input.GetKey("a")){
			SetMoveFlag(MoveFlag.Left);
		}else if(Input.GetKey("s")){
			SetMoveFlag(MoveFlag.Back);
		}else if(Input.GetKey("d")){
			SetMoveFlag(MoveFlag.Right);
		}else{
			SetMoveFlag(MoveFlag.Stay);
		}
		if(Input.GetKey("q")){
			SetMoveFlag(MoveFlag.LeftRotate);
		}
		if(Input.GetKey("e")){
			SetMoveFlag(MoveFlag.RightRotate);
		}
	}

	private void MoveAnimation(float speed){
		switch(moveFlag){
		case MoveFlag.Front:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.303f, 4.18f, -0.48f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(28.2006f, 00, 00)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(310.986f, -10.242f, 7.89311f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(383.985f, 1.33448f, -1.7887f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(332.395f, 358.787f, 4.86952f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(311.199f, 12.3595f, -9.4787f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(383.845f, 360.117f, 6.41597f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(332.460f, 1.48054f, -2.1350f)), Time.deltaTime * speed);
			break;
		case MoveFlag.Back:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.303f, 4.79f, -1.01f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(10.2383f, 00, 00)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(325.323f, 00, 00)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(362.448f, 359.566f, 1.13957f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(356.253f, -0.1870f, -0.0922f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(325.323f, 00, 00)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(362.387f, 1.67099f, 2.04901f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(356.271f, 358.848f, 3.46484f)), Time.deltaTime * speed);
			break;
		case MoveFlag.Left:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-1.2f, 4.69f, -0.65f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(20.0475f, 0.26924f, 11.0587f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(344.36f, 10.1843f, 5.65974f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(323.836f, 367.956f, 0.59759f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(369.649f, -2.1301f, -20.433f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(319.798f, -4.9462f, -12.813f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(375.326f, 2.50953f, 1.58415f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(336.625f, 358.76f, 2.74284f)), Time.deltaTime * speed);
			break;
		case MoveFlag.Right:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(1.31f, 4.82f, -0.48f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(25.9175f, -3.4569f, -11.576f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(321.547f, 8.96850f, 12.8154f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(368.144f, 358.628f, -8.5764f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(337.152f, -0.9468f, 4.58926f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(354.791f, -1.2246f, -7.7026f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(324.197f, 1.73391f, -2.3412f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(354.53f, 338.903f, 17.4425f)), Time.deltaTime * speed);
			break;
		case MoveFlag.FrontLeft:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.99f, 4.73f, 1.07f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(30.1132f, -1.3463f, 5.52754f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(336.746f, 11.2452f, 5.80075f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(341.198f, 356.053f, 0.44891f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(344.903f, 3.75918f, -15.752f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(317.876f, -5.0142f, -12.121f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(356.781f, 8.08473f, 4.7296f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(346.602f, 356.444f, 7.66204f)), Time.deltaTime * speed);
			break;
		case MoveFlag.FrontRight:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(1.07f, 4.82f, 0.43f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(33.2327f, -2.1644f, -9.2659f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(322.684f, 9.08021f, 12.9636f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(363.657f, 359.246f, -9.4025f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(333.261f, -0.7500f, 3.85824f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(355.282f, -9.8819f, 1.61767f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(332.849f, 4.01535f, -5.41f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(341.185f, 336.601f, 16.8334f)), Time.deltaTime * speed);
			break;
		case MoveFlag.BackLeft:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.92f, 4.69f, -1.44f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(13.2604f, 0.5494f, 10.9899f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(350.819f, 8.68089f, 7.5022f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(323.836f, 367.956f, 0.59759f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(369.649f, -2.1301f, -20.433f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(337.874f, -18.478f, -12.355f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(372.043f, 2.28353f, 2.7064f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(328.399f, 356.59f, 7.37332f)), Time.deltaTime * speed);
			break;
		case MoveFlag.BackRight:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(1.05f, 4.82f, -1.28f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(17.2954f, -2.45f, -8.3126f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(323.645f, 9.0623f, 13.1236f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(361.157f, 359.929f, -9.7707f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(350.383f, -0.0129f, 2.95802f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(345.806f, -7.1666f, -3.8258f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(314.652f, 0.44819f, -0.7427f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(378.836f, 348.774f, 8.6922f)), Time.deltaTime * speed);
			break;
		case MoveFlag.Stay:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.303f, 5.25f, -0.48f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(17.2642f, 00, 00)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(343.552f, 00, 00)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(346.307f, 1.71838f, 1.57249f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(346.353f, 359.210f, 3.34240f)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(343.552f, 00, 00)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(346.362f, 359.554f, 1.26321f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(346.330f, 00, 00)), Time.deltaTime * speed);
			break;
		case MoveFlag.RightRotate:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.303f, 4.9f, -0.48f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(17.2642f, 00, 00)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(332.525f, 3.16119f, 10.8701f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(341.850f, 362.007f, -4.4752f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(362.621f, -1.0051f, -2.8112f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(334.841f, 2.38279f, -15.653f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(340.526f, -8.7927f, 9.41601f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(362.825f, 357.410f, 5.89204f)), Time.deltaTime * speed);
			break;
		case MoveFlag.LeftRotate:
			pelvis.transform.localPosition = Vector3.Lerp(pelvis.transform.localPosition, new Vector3(-0.303f, 4.9f, -0.48f), Time.deltaTime * speed);
			pelvis.transform.localRotation = Quaternion.Lerp(pelvis.transform.localRotation, Quaternion.Euler(new Vector3(17.2642f, 00, 00)), Time.deltaTime * speed);
			rightLeg0.transform.localRotation = Quaternion.Lerp(rightLeg0.transform.localRotation, Quaternion.Euler(new Vector3(332.525f, 3.16119f, 10.8701f)), Time.deltaTime * speed);
			rightLeg1.transform.localRotation = Quaternion.Lerp(rightLeg1.transform.localRotation, Quaternion.Euler(new Vector3(341.850f, 362.007f, -4.4752f)), Time.deltaTime * speed);
			rightFoot.transform.localRotation = Quaternion.Lerp(rightFoot.transform.localRotation, Quaternion.Euler(new Vector3(362.621f, -1.0051f, -2.8112f)), Time.deltaTime * speed);
			leftLeg0.transform.localRotation = Quaternion.Lerp(leftLeg0.transform.localRotation, Quaternion.Euler(new Vector3(334.841f, 2.38279f, -15.653f)), Time.deltaTime * speed);
			leftLeg1.transform.localRotation = Quaternion.Lerp(leftLeg1.transform.localRotation, Quaternion.Euler(new Vector3(340.526f, -8.7927f, 9.41601f)), Time.deltaTime * speed);
			leftFoot.transform.localRotation = Quaternion.Lerp(leftFoot.transform.localRotation, Quaternion.Euler(new Vector3(362.825f, 357.410f, 5.89204f)), Time.deltaTime * speed);
			break;
		}
	}


}

public class MoveFlag{
	public const int Front = 0;
	public const int Left = 1;
	public const int Right = 2;
	public const int Back = 3;
	public const int FrontLeft = 4;
	public const int FrontRight = 5;
	public const int BackRight = 6;
	public const int BackLeft = 7;
	public const int Stay = 8;
	public const int RightRotate = 9;
	public const int LeftRotate = 10;
}


