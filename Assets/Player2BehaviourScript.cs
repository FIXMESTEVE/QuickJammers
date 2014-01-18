using UnityEngine;
using System.Collections;

public class Player2BehaviourScript : MonoBehaviour {
	private float playerSpeed = 10;
	private Vector3 move = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		
	}
	
	bool checkWallCollision(){
		if(transform.position.z + renderer.bounds.size.z/2 > GameObject.Find("TopBorder").transform.position.z - GameObject.Find ("TopBorder").renderer.bounds.size.z/2)
			return true;
		if(transform.position.z - renderer.bounds.size.z/2 < GameObject.Find("BottomBorder").transform.position.z + GameObject.Find("BottomBorder").renderer.bounds.size.z/2)
			return true;
		if(transform.position.x - renderer.bounds.size.x/2 < GameObject.Find("PlayerZoneLimiter").transform.position.x + GameObject.Find ("PlayerZoneLimiter").renderer.bounds.size.x/2)
			return true;
		if(transform.position.x + renderer.bounds.size.x/2 > GameObject.Find ("RightBorder").transform.position.x - GameObject.Find ("RightBorder").renderer.bounds.size.x/2)
			return true;
		return false;
	}

	// Update is called once per frame
	void Update () {
		if(!DiskBehaviourScript.p2Hold && !DiskBehaviourScript.p2Recovery){
			move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			transform.Translate(move * playerSpeed * Time.deltaTime);
			if(checkWallCollision())
				transform.Translate (move * playerSpeed * Time.deltaTime * -1);
			/*LEGACY INPUT MANAGEMENT
			 * It sucks but I kept it because reasons
			 * 
			 * if(Input.GetAxis("Vertical") > 0.25){
				transform.Translate(0, 0, playerSpeed);
				if(transform.position.z + renderer.bounds.size.z/2 > GameObject.Find("TopBorder").transform.position.z - GameObject.Find ("TopBorder").renderer.bounds.size.z/2)
					transform.Translate (0,0,-playerSpeed);
			}
			if(Input.GetAxis("Vertical") < -0.25){
				transform.Translate(0, 0, -playerSpeed);
				if(transform.position.z - renderer.bounds.size.z/2 < GameObject.Find("BottomBorder").transform.position.z + GameObject.Find("BottomBorder").renderer.bounds.size.z/2)
					transform.Translate(0,0, playerSpeed);
			}
			if(Input.GetAxis("Horizontal") < -0.25){
				transform.Translate(-playerSpeed, 0, 0);
				if(transform.position.x - renderer.bounds.size.x/2 < GameObject.Find("PlayerZoneLimiter").transform.position.x + GameObject.Find ("PlayerZoneLimiter").renderer.bounds.size.x/2)
					transform.Translate (playerSpeed, 0, 0);
			}
			if(Input.GetAxis("Horizontal") > 0.25){
				transform.Translate(playerSpeed, 0, 0);
				if(transform.position.x + renderer.bounds.size.x/2 > GameObject.Find ("RightBorder").transform.position.x - GameObject.Find ("RightBorder").renderer.bounds.size.x/2)
					transform.Translate (-playerSpeed, 0, 0);
			}
			*/
		}
		else if(DiskBehaviourScript.p2Hold){
			if(Input.GetAxis("Vertical") > 0.25 && !(Input.GetAxis("Horizontal") < -0.25))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_HARD;
			else if(Input.GetAxis("Vertical") < -0.25 && !(Input.GetAxis("Horizontal") < -0.25))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_HARD;
			else if(Input.GetAxis("Vertical") > 0.25 && (Input.GetAxis("Horizontal") < -0.25))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_SOFT;
			else if(Input.GetAxis("Vertical") < -0.25 && (Input.GetAxis("Horizontal") < -0.25))
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_SOFT;
			else
				DiskBehaviourScript.relaunchState = DiskRelaunchState.STRAIGHT;

			if(Input.GetButtonDown("360_AButton"))
				DiskBehaviourScript.relaunched = true;
		}
	}
}
