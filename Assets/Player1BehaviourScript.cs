using UnityEngine;
using System.Collections;

public class Player1BehaviourScript : MonoBehaviour {
	private float playerSpeed = 0.135f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!DiskBehaviourScript.p1Hold){
			if(Input.GetKey(KeyCode.Z)){
				transform.Translate(0, 0, playerSpeed);
			}
			if(Input.GetKey(KeyCode.S)){
				transform.Translate(0, 0, -playerSpeed);
			}
			if(Input.GetKey(KeyCode.Q)){
				transform.Translate(-playerSpeed, 0, 0);
			}
			if(Input.GetKey(KeyCode.D)){
				transform.Translate(playerSpeed, 0, 0);
			}
		}
		else{
			if(Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.D))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_HARD;
			else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_HARD;
			else if(Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.D))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_SOFT;
			else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_SOFT;
			else
				DiskBehaviourScript.relaunchState = DiskRelaunchState.STRAIGHT;

			if(Input.GetKey(KeyCode.Space))
				DiskBehaviourScript.relaunched = true;
		}
	}
}
