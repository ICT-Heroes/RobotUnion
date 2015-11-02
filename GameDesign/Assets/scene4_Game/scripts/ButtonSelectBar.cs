using UnityEngine;
using System.Collections;

public class ButtonSelectBar : MonoBehaviour {
	
	public GameObject[] buttonCover;

	void Start () {
		SetButton(0);
	}

	void Update () {
	
	}

	public void SetButton(int num){
		for(int i = 0; i < buttonCover.Length ; i ++){
			buttonCover[i].SetActive(true);
		}
		buttonCover[num].SetActive(false);
	}
}
