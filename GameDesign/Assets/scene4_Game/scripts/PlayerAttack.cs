using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public Transform[] bullet;
	public GameObject gunPoint;
	public RectTransform powerBar;
	public GameObject button_Ball_Bar;
	public GameObject button_Attack_Bar;
	public int buckShotCount = 10;

	private float power = 1;
	private int attackMode = 0;
	private int ballMode = 0;
	private int myBuckShotCount;

	void Start () {
		power = 0;
		PowerBarReDraw();
	}
	
	void Update () {
		Attack();
		AttackModeChange();
		BallModeChange();
	}

	private void Attack(){
		switch(attackMode){
		case 0:
			if(Input.GetMouseButtonUp(0)){
				power = 1;
				ShootBall();
			}
			break;
		case 1:
			break;
		case 2:
			if(Input.GetMouseButton(0)){
				power += (1-power)*Time.deltaTime*3;
				PowerBarReDraw();
			}
			if(Input.GetMouseButtonUp(0)){
				ShootBall();
				power = 0;
				PowerBarReDraw();
			}
			break;
		case 3:
			if(Input.GetMouseButtonUp(0)){
				myBuckShotCount = 0;
			}
			break;
		}

		if(myBuckShotCount < buckShotCount){
			myBuckShotCount++;
			ShootBall();
		}
	}

	private void AttackModeChange(){
		if(Input.GetKey("q")){
			attackMode = 0;
			button_Attack_Bar.GetComponent<ButtonSelectBar>().SetButton(attackMode);
		}
		if(Input.GetKey("w")){
			attackMode = 1;
			button_Attack_Bar.GetComponent<ButtonSelectBar>().SetButton(attackMode);
		}
		if(Input.GetKey("e")){
			power = 0;
			attackMode = 2;
			button_Attack_Bar.GetComponent<ButtonSelectBar>().SetButton(attackMode);
		}
		if(Input.GetKey("r")){
			attackMode = 3;
			button_Attack_Bar.GetComponent<ButtonSelectBar>().SetButton(attackMode);
		}
	}

	private void BallModeChange(){
		if(Input.GetKey("1")){
			ballMode = 0;
			button_Ball_Bar.GetComponent<ButtonSelectBar>().SetButton(ballMode);
		}
		if(Input.GetKey("2")){
			ballMode = 1;
			button_Ball_Bar.GetComponent<ButtonSelectBar>().SetButton(ballMode);
		}
		if(Input.GetKey("3")){
			ballMode = 2;
			button_Ball_Bar.GetComponent<ButtonSelectBar>().SetButton(ballMode);
		}
		if(Input.GetKey("4")){
			ballMode = 3;
			button_Ball_Bar.GetComponent<ButtonSelectBar>().SetButton(ballMode);
		}
	}

	private void PowerBarReDraw(){
		powerBar.transform.localScale = new Vector3(power, 1, 1);
	}
	
	private void ShootBall(){
		CreateBall().GetComponent<Rigidbody>().AddForce(gunPoint.transform.forward * 3000 * power);
	}

	private Transform CreateBall(){
		GameObject newBullet = null;
		switch(ballMode){
		case 0:
			newBullet  = ((Transform)Instantiate(bullet[0], gunPoint.transform.position, Quaternion.identity)).gameObject;
			break;
		case 1:
			newBullet  = ((Transform)Instantiate(bullet[1], gunPoint.transform.position, Quaternion.identity)).gameObject;
			break;
		case 2:
			newBullet  = ((Transform)Instantiate(bullet[2], gunPoint.transform.position, Quaternion.identity)).gameObject;
			break;
		case 3:
			newBullet  = ((Transform)Instantiate(bullet[3], gunPoint.transform.position, Quaternion.identity)).gameObject;
			break;
		}
		newBullet.transform.forward = gunPoint.transform.forward;
		return newBullet.transform;
	}
}
