using UnityEngine;
using System.Collections;

public class DiskBehaviourScript : MonoBehaviour {
	private float xSpeed = 0.135f;
	private float zSpeed = 0.135f;
	private Vector3 initTransformPos;
	// Use this for initialization
	void Start () {
		initTransformPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(xSpeed,0,zSpeed);
	}

	void OnCollisionEnter (Collision col){
		if(col.gameObject.name == "TopBorder" || col.gameObject.name == "BottomBorder")
			zSpeed *= -1;
		if(col.gameObject.name == "Player1" || col.gameObject.name == "Player2")
			xSpeed *= -1;
		if(col.gameObject.name == "LeftBorder" || col.gameObject.name == "RightBorder")
			transform.position = initTransformPos;
	}
}
