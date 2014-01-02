using UnityEngine;
using System.Collections;

public class Player2BehaviourScript : MonoBehaviour {
	private float playerSpeed = 0.135f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!DiskBehaviourScript.p1Hold){
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
	}
}
