using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject shot;			//shot that the enemy fires
	public Transform shotSpawn;		//location of where the shot will spawn in relation to the player

	private float nextFire = 0;		//when allowed to fire next shot
	public float fireRate;			//how long between shots fired			

	public void Shoot(){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, Quaternion.identity);
		}
	}
		
	void Update (){
		Shoot ();
	}
}
