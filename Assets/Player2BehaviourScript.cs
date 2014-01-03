using UnityEngine;
using System.Collections;

public class Player2BehaviourScript : MonoBehaviour {
	private float playerSpeed = 0.135f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!DiskBehaviourScript.p2Hold){
			if(Input.GetKey(KeyCode.UpArrow)){
				transform.Translate(0, 0, playerSpeed);
			}
			if(Input.GetKey(KeyCode.DownArrow)){
				transform.Translate(0, 0, -playerSpeed);
			}
			if(Input.GetKey(KeyCode.LeftArrow)){
				transform.Translate(-playerSpeed, 0, 0);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				transform.Translate(playerSpeed, 0, 0);
			}
		}
		else{
			if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_HARD;
			else if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_HARD;
			else if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_SOFT;
			else if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_SOFT;
			else
				DiskBehaviourScript.relaunchState = DiskRelaunchState.STRAIGHT;

			if(Input.GetKey(KeyCode.RightCommand))
				DiskBehaviourScript.relaunched = true;
		}
	}
}
