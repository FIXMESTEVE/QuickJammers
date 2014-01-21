using UnityEngine;
using System.Collections;

public class Player1BehaviourScript : MonoBehaviour {
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
			if(Input.GetKeyDown(KeyCode.LeftShift) && (move.magnitude > 0.0) && !dashing && !DiskBehaviourScript.p1Hold)
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
		if(!dashing)
			move = new Vector3(Input.GetAxis("Horizontal_Keyboard"), 0, Input.GetAxis("Vertical_Keyboard"));

		transform.Translate(move * playerSpeed * Time.deltaTime);

		if(DiskBehaviourScript.p1Hold || DiskBehaviourScript.p1Recovery)
			if(!dashing)
				transform.Translate(move * playerSpeed * Time.deltaTime * -1);

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

		if(DiskBehaviourScript.p1Hold){
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
			else if (Input.GetKey(KeyCode.E) && !dashing){
				DiskBehaviourScript.relaunchState = DiskRelaunchState.LOB;
				DiskBehaviourScript.relaunched = true;
			}
			else{
				DiskBehaviourScript.relaunchState = DiskRelaunchState.STRAIGHT;
			}
			if(Input.GetKeyDown(KeyCode.Space) && !dashing)
				DiskBehaviourScript.relaunched = true;
		}
	}
}
