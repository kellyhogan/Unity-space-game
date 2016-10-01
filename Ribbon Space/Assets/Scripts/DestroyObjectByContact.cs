using UnityEngine;
using System.Collections;

public class DestroyObjectByContact : MonoBehaviour {

	private GameController gameController;			//reference to object of type GameController 

	public GameObject explosion;					//explosion game object

	//public int pointsEarned;						//points earned by destruction(to be implemented upon expansion)


	void Start ()
	{
		//make sure the GameController object exists before I try to access it
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
	//destroys when another collider enters this collider
	void OnTriggerEnter(Collider otherCol)
	{
		//cases:

		//if a game object is in the boundary, do not destroy it
		if (otherCol.tag == "Boundary"){
			return;
		}

		//if any object collides with stars, do not destoy it
		if (otherCol.tag == "Stars") {
			return;
		}
			
		//blocks cannot destroy each other
		if ((gameObject.tag == "Block" && otherCol.tag == "Block") || (gameObject.tag == "Block" && otherCol.tag == "Block")) {
			return;
		}
			
		//if the player collides with anything but stars, it is game over	
		if (gameObject.tag == "Player" && otherCol.tag != "Stars") {
			gameController.GameOver();
		}

		//destroy:
		//create an explosion at the location of the game object to be destroyed
		Instantiate(explosion, transform.position, transform.rotation); 

		//if any game object hits the block, destroy it
		if (otherCol.tag != "Block") {
			Destroy (otherCol.gameObject);
		}

		//destroy game object this script is attached to but don't destroy the blocks(since the blocks have this script too)
		if(gameObject.tag != "Block"){
			Destroy (gameObject);
		}
	}
}
