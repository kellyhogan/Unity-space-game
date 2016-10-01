using UnityEngine;
using System.Collections;

public class AddPointsCollision : MonoBehaviour {

	private GameController gameController;		//reference to object of type GameController 

	public int pointsEarned;					//points earned by collecting this game object




	// Use this for initialization
	void Start () {
		//make sure the game controller exists before trying to access it
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	
	}

	//add points if this game object collides with player
	void OnTriggerEnter(Collider otherCol){
		if (gameObject.tag == "Stars" && otherCol.tag == "Player") {
			gameController.AddScore (pointsEarned);

		}
	}
}
