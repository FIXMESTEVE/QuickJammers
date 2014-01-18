using UnityEngine;
using System.Collections;

public class Player1BehaviourScript : MonoBehaviour {
	private float playerSpeed = 10;
	private Vector3 move = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}

	bool checkWallCollision(){
		if(transform.position.z + renderer.bounds.size.z/2 > GameObject.Find("TopBorder").transform.position.z - GameObject.Find("TopBorder").renderer.bounds.size.z/2)
			return true;
		if(transform.position.z - renderer.bounds.size.z/2 < GameObject.Find("BottomBorder").transform.position.z + GameObject.Find("BottomBorder").renderer.bounds.size.z/2)
			return true;
		if(transform.position.x - renderer.bounds.size.x/2 < GameObject.Find("LeftBorder").transform.position.x + GameObject.Find("LeftBorder").renderer.bounds.size.x/2)
			return true;
		if(transform.position.x + renderer.bounds.size.x/2 > GameObject.Find("PlayerZoneLimiter").transform.position.x - GameObject.Find("PlayerZoneLimiter").renderer.bounds.size.x/2)
			return true;
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!DiskBehaviourScript.p1Hold && !DiskBehaviourScript.p1Recovery){
			move = new Vector3(Input.GetAxis("Horizontal_Keyboard"), 0, Input.GetAxis("Vertical_Keyboard"));
			transform.Translate(move * playerSpeed * Time.deltaTime);
			if(checkWallCollision())
				transform.Translate (move * playerSpeed * Time.deltaTime * -1);
			/*LEGACY INPUT MANAGEMENT
			 * It sucks but I kept it because reasons
			 * 
			 * if(Input.GetKey(KeyCode.Z)){
				transform.Translate(0, 0, playerSpeed);
				if(transform.position.z + renderer.bounds.size.z/2 > GameObject.Find("TopBorder").transform.position.z - GameObject.Find("TopBorder").renderer.bounds.size.z/2)
					transform.Translate(0, 0, -playerSpeed);
			}
			if(Input.GetKey(KeyCode.S)){
				transform.Translate(0, 0, -playerSpeed);
				if(transform.position.z - renderer.bounds.size.z/2 < GameObject.Find("BottomBorder").transform.position.z + GameObject.Find("BottomBorder").renderer.bounds.size.z/2)
					transform.Translate(0, 0, playerSpeed);
			}
			if(Input.GetKey(KeyCode.Q)){
				transform.Translate(-playerSpeed, 0, 0);
				if(transform.position.x - renderer.bounds.size.x/2 < GameObject.Find("LeftBorder").transform.position.x + GameObject.Find("LeftBorder").renderer.bounds.size.x/2)
					transform.Translate(playerSpeed, 0, 0);
			}
			if(Input.GetKey(KeyCode.D)){
				transform.Translate(playerSpeed, 0, 0);
				if(transform.position.x + renderer.bounds.size.x/2 > GameObject.Find("PlayerZoneLimiter").transform.position.x - GameObject.Find("PlayerZoneLimiter").renderer.bounds.size.x/2)
					transform.Translate(-playerSpeed, 0, 0);
			}
			*/
		}
		else if(DiskBehaviourScript.p1Hold){
			if(Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.D)){
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_HARD;
			}
			else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)){
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_HARD;
			}
			else if(Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.D)){
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_UP_SOFT;
			}
			else if(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)){
				DiskBehaviourScript.relaunchState = DiskRelaunchState.DIAG_DOWN_SOFT;
			}
			else{
				DiskBehaviourScript.relaunchState = DiskRelaunchState.STRAIGHT;
			}
			if(Input.GetKeyDown(KeyCode.Space))
				DiskBehaviourScript.relaunched = true;
		}
	}
}
