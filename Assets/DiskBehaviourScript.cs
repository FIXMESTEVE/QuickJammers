using UnityEngine;
using System.Collections;

public enum DiskRelaunchState{
	STRAIGHT, DIAG_UP_SOFT, DIAG_DOWN_SOFT, DIAG_UP_HARD, DIAG_DOWN_HARD
};

public class DiskBehaviourScript : MonoBehaviour {

	//global variables
	public static bool p1Hold = false;
	public static bool p2Hold = false;
	public static bool p1Recovery = false;
	public static bool p2Recovery = false;
	public static bool relaunched = false;
	public static DiskRelaunchState relaunchState = DiskRelaunchState.STRAIGHT;

	//scores
	private int p1Score = 0; private int p2Score = 0;

	//prefabs
	public GameObject explosion;
	public GameObject wallBounceSparks;

	//timers
	private float initTimer;
	private float freezeTimer = 1;
	private float p1RecoveryTimer = 0.2f;
	private float p2RecoveryTimer = 0.2f;

	private float cSpeed = 20;
	private float initSpeed;
	private Vector3 initTransformPos;

	// Use this for initialization
	void Start () {
		p1Hold = true;
		initSpeed = cSpeed;
		initTimer = freezeTimer;
		transform.Find ("JustFrameRelaunchTrail").GetComponent<TrailRenderer>().enabled = false;
		gameObject.GetComponent<TrailRenderer>().enabled=false;
		GameObject.Find("Player1Score").guiText.text = "P1 Score: "+ p1Score.ToString();
		GameObject.Find("Player2Score").guiText.text = "P2 Score: "+ p2Score.ToString();
	}

	void freezeDisk(){
		/*rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.Sleep();*/
		rigidbody.isKinematic = true;
	}

	void relaunchDisk(){
		rigidbody.isKinematic = false;
		if (relaunchState == DiskRelaunchState.STRAIGHT){
			if(p1Hold){
				rigidbody.AddForce(10, 0, 0);
			}
			else if (p2Hold)
				rigidbody.AddForce(-10, 0, 0); 
		}
		else if(relaunchState == DiskRelaunchState.DIAG_DOWN_HARD){
			if(p1Hold)
				rigidbody.AddForce(10, 0, -15);
			else if (p2Hold)
				rigidbody.AddForce(-10, 0, -15);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_UP_HARD){
			if(p1Hold)
				rigidbody.AddForce(10, 0, 15);
			else if (p2Hold)
				rigidbody.AddForce(-10, 0, 15);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_DOWN_SOFT){
			if(p1Hold)
				rigidbody.AddForce(10, 0, -10);
			else if (p2Hold) 
				rigidbody.AddForce(-10, 0, -10);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_UP_SOFT){
			if(p1Hold)
				rigidbody.AddForce(10, 0, 10);
			else if (p2Hold)
				rigidbody.AddForce(-10, 0, 10);
		}
		Debug.Log (freezeTimer);
		if(freezeTimer > initTimer - initTimer/20){ //todo: save the initial timer
			cSpeed = initSpeed + 5;
			transform.Find ("JustFrameRelaunchTrail").GetComponent<TrailRenderer>().enabled = true;
			gameObject.GetComponent<TrailRenderer>().enabled=false;
		}
		else if(freezeTimer < initTimer/2){
			transform.Find ("JustFrameRelaunchTrail").GetComponent<TrailRenderer>().enabled = false;
			gameObject.GetComponent<TrailRenderer>().enabled=false;
			cSpeed = (float)(initSpeed/1.5);
		}
		else{
			transform.Find ("JustFrameRelaunchTrail").GetComponent<TrailRenderer>().enabled = false;
			gameObject.GetComponent<TrailRenderer>().enabled=true;
			cSpeed = (float)(initSpeed * freezeTimer * 1.1);
		}
	}

	// Update is called once per frame
	void Update () {
		//If the player catches the ball, we position it in front of him
		if(p1Hold){
			transform.Find ("JustFrameRelaunchTrail").GetComponent<TrailRenderer>().enabled = false;
			gameObject.GetComponent<TrailRenderer>().enabled=false;
			freezeDisk();
			transform.position = new Vector3(GameObject.Find("Player1").transform.position.x + 1.2f, transform.position.y, GameObject.Find("Player1").transform.position.z);
			freezeTimer -= Time.deltaTime;
		}
		else if(p2Hold){
			transform.Find ("JustFrameRelaunchTrail").GetComponent<TrailRenderer>().enabled = false;
			gameObject.GetComponent<TrailRenderer>().enabled=false;
			freezeDisk();
			transform.position = new Vector3(GameObject.Find("Player2").transform.position.x - 1.2f, transform.position.y, GameObject.Find("Player2").transform.position.z);
			freezeTimer -= Time.deltaTime;
		}
		else if (!p1Hold && !p2Hold){
			rigidbody.velocity = rigidbody.velocity.normalized * cSpeed;
		}

		if(p1Hold){
			if(freezeTimer <= 0 || relaunched){
				p1Recovery = true;
				relaunchDisk();
				p1Hold = false;
				freezeTimer = 1;
				relaunched = false;
				//Debug.Log("p1Hold timer end!");
			}
		}
		else if(p2Hold){
			if(freezeTimer <= 0 || relaunched){
				p2Recovery = true;
				relaunchDisk();
				p2Hold = false;
				freezeTimer = 1;
				relaunched = false;
				//Debug.Log("p2Hold timer end!");
			}
		}

		if(p1Recovery){
			if(p1RecoveryTimer > 0){
				p1RecoveryTimer -= Time.deltaTime;
			}
			else{
				p1Recovery = false;
				p1RecoveryTimer = 0.2f;
			}
		}

		if(p2Recovery){
			if(p2RecoveryTimer > 0){
				p2RecoveryTimer -= Time.deltaTime;
			}
			else{
				p2Recovery = false;
				p2RecoveryTimer = 0.2f;
			}
		}

		Debug.Log ("p1Hold: " + p1Hold + "; p2Hold: " + p2Hold + "; p1Recovery: " + p1Recovery +
		           "; p2Recovery: " + p2Recovery + "; relaunched: " + relaunched );
		           //"; p1RecoveryTimer: " + (p1RecoveryTimer) + 
		           //"; p2RecoveryTimer: " + (p2RecoveryTimer) + "; freezeTimer: " + freezeTimer);
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == "Player1" ){
			p1Hold = true;
		}
		if(col.gameObject.name == "Player2" ){
			//freezeDisk();
			p2Hold = true;
		}

		if(col.gameObject.name == "LeftBorder"){
			Instantiate(explosion, transform.position, transform.rotation);
			p1Hold = true;
			p2Score++;
			GameObject.Find("Player2Score").guiText.text = "P2 Score: "+ p2Score.ToString();
		}

		if(col.gameObject.name == "RightBorder"){
			Instantiate(explosion, transform.position, transform.rotation);
			p2Hold = true;
			p1Score++;
			GameObject.Find("Player1Score").guiText.text = "P1 Score: "+ p1Score.ToString();
		}

		if(col.gameObject.name == "TopBorder" || col.gameObject.name == "BottomBorder")
			Instantiate(wallBounceSparks, transform.position, transform.rotation);
	}
}
