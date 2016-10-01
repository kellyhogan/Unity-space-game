using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;		//the rigidbody of the gameobject this script is attached to(player)
	private MeshCollider col;			//for changing the color of the player when it is invincible
	private Renderer rend;				//for invincibility
	public Transform shotSpawn;			//location of where the shot is spawned in relation to the player

	public Material[] materials;		//array of materials so the player can change color when invinsible	
	public GameObject [] shots;			//array of different shots that the player can fire. They look different
										//bit perform the same function

	private float nextFire = 0;			//when allowed to fire next shot

	public float speed;					//speed of player
	//border boundaries
	public float left;
	public float right;
	public float top;
	public float bottom;
	//public shooting variables
	public float fireRate;				//how long between shots fired


	//Good for testing game. Disables the player's collider
	public void Invincible(){
		if (Input.GetButton ("Click")) {		//"Click" is the "alt" key
			col.enabled = false;
			rend.sharedMaterial = materials[1];
		}
		if (Input.GetButtonUp ("Click")) {
			col.enabled = true;
			rend.sharedMaterial = materials[0];
		}
	}


	//Shoot when space bar is pressed and limit shot rate
	public void Shoot(){
	
		//shoot if space bar is pressed and if time in game is greater nextFire(when allowed to fire next shot)
		if (Input.GetButton ("SpaceBar") && Time.time > nextFire) {		//Time.time is time in seconds since the start of the game
			//update nextFire
			nextFire = Time.time + fireRate;
			//pick a random shot from the array of shots
			GameObject toInstantiate = shots[Random.Range (0,shots.Length)];
			//create an instance of the selected shot at shotSpawn without rotation
			Instantiate(toInstantiate, shotSpawn.position, Quaternion.identity);
		}
	}

	//Set a border that the player cannot leave
	public void SetBorder(){
		rigidBody.position = new Vector3 (
			Mathf.Clamp (rigidBody.position.x, left, right),
			Mathf.Clamp (rigidBody.position.y, bottom, top),
			0f
		);
	}

	//move the player
	public void MovePlayer(){
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//set velocity
		Vector3 basicMovement = new Vector3 (0f, moveVertical, 0f);
		rigidBody.velocity = basicMovement * speed;
	}

	//if the player collides with the stars, deactiveate the star (disable every component)
	//to make it seem as if the player collected the star
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Stars")){
				other.gameObject.SetActive (false);
			}
	}

	//in the first frame
	void Start(){
		rigidBody = GetComponent<Rigidbody> ();			//access the local Rigidbody component
		col = GetComponent<MeshCollider> ();			//access the local MeshCollider component
		//at the start, the collider is enabled
		col.enabled = true;
		rend = GetComponent<Renderer>();				//access the local Renderer component
		//at the start, the player is visable
		rend.enabled = true;
	}

	//called once per frame (on irregular timeline i.e. if one frame is lagging, things won't update)
	void Update (){
		Shoot ();
		Invincible ();	
	}

	//use this for physics(rigidbodies). called on a regular timeline
	void FixedUpdate (){
		SetBorder ();
		MovePlayer ();
	}
}