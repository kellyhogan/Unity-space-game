using UnityEngine;
using System.Collections;

public class DestroyIfLeaveScreen : MonoBehaviour {

	//when another gameobject2 exits this game object, destroy the gameobject2
	void OnTriggerExit(Collider otherCol)
	{
		Destroy(otherCol.gameObject);
	}
}
