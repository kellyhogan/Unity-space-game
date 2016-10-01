using UnityEngine;
using System.Collections;

//this class allows game objects to move on the x-axis with a certain speed
public class Move : MonoBehaviour {


	//reference to Rigidbody
	private Rigidbody rigidBody;

	//manipulate in editor
	public float speed;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		//velocity of the shot is the direction of the x axis multiplied by speed
		rigidBody.velocity = transform.right * speed;
	}



}
