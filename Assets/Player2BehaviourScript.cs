using UnityEngine;
using System.Collections;

public class Player2BehaviourScript : MonoBehaviour {
	private float playerSpeed = 6;
	private Vector3 move = Vector3.zero;
	public static bool dashing = false;
	
	float normalSpeed = 6;
	float dashForSeconds = 0.2f;
	float dashRebuildTime = 10.0f;
	float dashSpeed = 25.0f;
	float dashAmount = 1.0f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("dash");
	}
	
	IEnumerator dash(){
		while (true){
			playerSpeed = normalSpeed;
			if(Input.GetButtonDown("360_BButton") && (move.magnitude > 0.0) && !dashing && !DiskBehaviourScript.p2Hold)
				dashing = true;
			
			if(dashing){
				playerSpeed = dashSpeed;
				
				
				dashAmount -= Time.deltaTime / dashForSeconds;
				if(dashAmount <= 0.0){
					playerSpeed = normalSpeed;
					dashing = false;
					dashAmount = 1.0f;
				}
			}
			
			//if(move.magnitude > 0.0) dashAmount += Time.deltaTime / dashRebuildTime;
			dashAmount = Mathf.Clamp01(dashAmount);
			Debug.Log (dashing + " " + dashAmount);
			yield return null;
			
		}
		
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
		if(!dashing)
			move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		transform.Translate(move * playerSpeed * Time.deltaTime);
		
		if(DiskBehaviourScript.p2Hold || DiskBehaviourScript.p2Recovery)
			if(!dashing)
				transform.Translate(move * playerSpeed * Time.deltaTime * -1);
		
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

		if(DiskBehaviourScript.p2Hold){
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

			if(Input.GetButtonDown("360_AButton") && !dashing)
				DiskBehaviourScript.relaunched = true;
		}
	}
}
