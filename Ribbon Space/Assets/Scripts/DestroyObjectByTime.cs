using UnityEngine;
using System.Collections;

public class DestroyObjectByTime : MonoBehaviour {

	public float duration;		//time to wait before destruction

	public void Start (){
		//destroy this game object after duration
		Destroy (gameObject, duration);		
	}


	

}
