using UnityEngine;
using System.Collections;

public class Player1BehaviourScript : MonoBehaviour {
	private float playerSpeed = 0.135f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!DiskBehaviourScript.p1Hold && !DiskBehaviourScript.p1Recovery){
			if(Input.GetKey(KeyCode.Z)){
				transform.Translate(0, 0, playerSpeed);
				if(transform.position.z + transform.renderer.bounds.size.z > GameObject.Find("TopBorder").transform.position.z)
					transform.Translate(0, 0, -playerSpeed);
			}
			if(Input.GetKey(KeyCode.S)){
				transform.Translate(0, 0, -playerSpeed);
				if(transform.position.z < GameObject.Find("BottomBorder").transform.position.z + GameObject.Find("BottomBorder").renderer.bounds.size.z)
					transform.Translate(0, 0, playerSpeed);
			}
			if(Input.GetKey(KeyCode.Q)){
				transform.Translate(-playerSpeed, 0, 0);
				if(transform.position.x < GameObject.Find("LeftBorder").transform.position.x + GameObject.Find("LeftBorder").renderer.bounds.size.x)
					transform.Translate(playerSpeed, 0, 0);
			}
			if(Input.GetKey(KeyCode.D)){
				transform.Translate(playerSpeed, 0, 0);
				if(transform.position.x + transform.renderer.bounds.size.x > GameObject.Find("PlayerZoneLimiter").transform.position.x)
					transform.Translate(-playerSpeed, 0, 0);
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
