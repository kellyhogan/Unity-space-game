using UnityEngine;
using System.Collections;

public class ManipulateCrate : MonoBehaviour {

	public float speed;

	public void RotateComponent(){

		transform.Rotate(new Vector3(0f ,-30f, 15f) * speed);
	}

	// Update is called once per frame
	void FixedUpdate () {
		RotateComponent ();
	}
}
