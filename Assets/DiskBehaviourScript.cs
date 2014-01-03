using UnityEngine;
using System.Collections;

public enum DiskRelaunchState{
	STRAIGHT, DIAG_UP_SOFT, DIAG_DOWN_SOFT, DIAG_UP_HARD, DIAG_DOWN_HARD
};

public class DiskBehaviourScript : MonoBehaviour {

	public static bool p1Hold = false;
	public static bool p2Hold = false;
	public static DiskRelaunchState relaunchState = DiskRelaunchState.STRAIGHT;

	private float freezeTimer = 1;
	private float cSpeed = 20;
	private Vector3 initTransformPos;
	// Use this for initialization
	void Start () {
		initTransformPos = transform.position;
		rigidbody.AddForce(10, 0, 10);
	}

	void freezeDisk(){
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.Sleep();
	}

	void relaunchDisk(){
		if (relaunchState == DiskRelaunchState.STRAIGHT){
			if(p1Hold)
				rigidbody.AddForce(10, 0, 0);
			else if (p2Hold) rigidbody.AddForce(-10, 0, 0); 
		}
		else if(relaunchState == DiskRelaunchState.DIAG_DOWN_HARD){
			if(p1Hold)
				rigidbody.AddForce(10, 0, -10);
			else if (p2Hold) rigidbody.AddForce(-10, 0, -10);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_UP_HARD){
			if(p1Hold)
				rigidbody.AddForce(10, 0, 10);
			else if (p2Hold) rigidbody.AddForce(-10, 0, 10);
		}
	}

	// Update is called once per frame
	void Update () {
		if(p1Hold && freezeTimer <= 0){
			relaunchDisk();
			p1Hold = false; freezeTimer = 1; 
			//rigidbody.AddForce(10, 0, 10); 
			Debug.Log("Timer end!");
		}
		else if(p2Hold && freezeTimer <= 0){
			relaunchDisk();
			p2Hold = false; freezeTimer = 1; 
			//rigidbody.AddForce(-10, 0, -10); 
			Debug.Log("Timer end!");
		}


		if(p1Hold){
			transform.position = new Vector3(GameObject.Find("Player1").transform.position.x + 1.1f, transform.position.y, GameObject.Find("Player1").transform.position.z);
			freezeTimer -= Time.deltaTime;
		}
		else if(p2Hold){
			transform.position = new Vector3(GameObject.Find("Player2").transform.position.x - 1.1f, transform.position.y, GameObject.Find("Player2").transform.position.z);
			freezeTimer -= Time.deltaTime;
		}
		else if (!p1Hold && !p2Hold){
			rigidbody.velocity = rigidbody.velocity.normalized * cSpeed;
		}
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == "Player1" ){
			freezeDisk();
			p1Hold = true;
		}
		if(col.gameObject.name == "Player2" ){
			freezeDisk();
			p2Hold = true;
		}
		if(col.gameObject.name == "LeftBorder" || col.gameObject.name == "RightBorder")
			transform.position = initTransformPos;
	}
}
