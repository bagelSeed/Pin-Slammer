using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour {

	public GameObject player;		//Public variable to store a reference to the player game object

	private Vector3 offset;			//Private variable to store the offset distance between the player and camera
	
	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        offset = player.transform.position - transform.position;
        transform.position += new Vector3(offset.x, offset.y, 0);
	}
}
