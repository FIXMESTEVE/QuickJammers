using UnityEngine;
using System.Collections;

public enum DiskRelaunchState{
	STRAIGHT, DIAG_UP_SOFT, DIAG_DOWN_SOFT, DIAG_UP_HARD, DIAG_DOWN_HARD
};

public class DiskBehaviourScript : MonoBehaviour {

	public static bool p1Hold = false;
	public static bool p2Hold = false;
	public static bool relaunched = false;
	public static DiskRelaunchState relaunchState = DiskRelaunchState.STRAIGHT;

	private int p1Score = 0; private int p2Score = 0;
	private float freezeTimer = 1;
	private float recoveryTimer = 1.2f;
	private float cSpeed = 20;
	private Vector3 initTransformPos;
	// Use this for initialization
	void Start () {
		p1Hold = true;
		rigidbody.AddForce(10, 0, 10);
		GameObject.Find("Player1Score").guiText.text = "P1 Score: "+ p1Score.ToString();
		GameObject.Find("Player2Score").guiText.text = "P2 Score: "+ p2Score.ToString();
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
				rigidbody.AddForce(10, 0, -15);
			else if (p2Hold) rigidbody.AddForce(-10, 0, -15);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_UP_HARD){
			if(p1Hold)
				rigidbody.AddForce(10, 0, 15);
			else if (p2Hold) rigidbody.AddForce(-10, 0, 15);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_DOWN_SOFT){
			if(p1Hold)
				rigidbody.AddForce(10, 0, -10);
			else if (p2Hold) rigidbody.AddForce(-10, 0, -10);
		}
		else if(relaunchState == DiskRelaunchState.DIAG_UP_SOFT){
			if(p1Hold)
				rigidbody.AddForce(10, 0, 10);
			else if (p2Hold) rigidbody.AddForce(-10, 0, 10);
		}
	}

	// Update is called once per frame
	void Update () {
		if((p1Hold && freezeTimer <= 0) || (p1Hold && relaunched)){
			relaunchDisk();
			p1Hold = false; freezeTimer = 1; relaunched =  false;
			Debug.Log("Timer end!");
		}
		else if((p2Hold && freezeTimer <= 0) || (p2Hold && relaunched)){
			relaunchDisk();
			p2Hold = false; freezeTimer = 1; relaunched = false;
			Debug.Log("Timer end!");
		}

		//If the player catches the ball, we position it in front of him
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

		if(col.gameObject.name == "LeftBorder"){
			p1Hold = true;
			p2Score++;
			GameObject.Find("Player2Score").guiText.text = "P2 Score: "+ p2Score.ToString(); 
		}

		if(col.gameObject.name == "RightBorder"){
			p2Hold = true;
			p1Score++;
			GameObject.Find("Player1Score").guiText.text = "P1 Score: "+ p1Score.ToString();
		}
	}
}
