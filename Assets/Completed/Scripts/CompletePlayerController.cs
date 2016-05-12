using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class CompletePlayerController : MonoBehaviour {

	public float speed;				
    public float friction;
    public float percentage;        

    public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.

    LineRenderer lineRenderer;
    int lineCount = 0;

    public Camera camera;

    Vector2 direction;

    // Use this for initialization
    void Start()
	{
        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Player";
        lineRenderer.sortingOrder = 1;
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
		//Initialize count to zero.
		count = 0;
		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";
		//Call our SetCountText function which will update the text with the current value for count.
		SetCountText ();
        spawnPlayer();
    }

    void OnMouseDrag()
    {
        lineRenderer.SetVertexCount(2);

        direction = transform.position - camera.ScreenToWorldPoint(Input.mousePosition);

        if (direction.magnitude > 7)
        { 
            direction = direction.normalized * 7;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector2(transform.position.x + direction.x, transform.position.y + direction.y));
    }

    void OnMouseUp()
    {
        // World pos - world pos
        Vector2 movement = direction * speed * percentage;
        rb2d.AddForce(movement);
        lineRenderer.SetVertexCount(0);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = rb2d.velocity.x;
		//Store the current vertical input in the float moveVertical.
        float moveVertical = rb2d.velocity.y;
		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity = movement * (1 - friction / 10);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);
			//Add one to the current value of our count variable.
			count = count + 1;
			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}
        else if (other.gameObject.CompareTag("Background"))
        {
            killPlayer();
        }
	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		countText.text = "Count: " + count.ToString ();
		//Check if we've collected all 12 pickups. If we have...
		if (count >= 12)
			//... then set the text property of our winText object to "You win!"
			winText.text = "You win!";
	}

    void killPlayer()
    {
        // Maybe reset a bunch of crap here:
        // Fade player and then spawn if possible
        shrinkPlayer();
    }

    void shrinkPlayer()
    {
        spawnPlayer();
    }

    void spawnPlayer()
    {
        // Eventually get some kind of init position getting for each map
        Vector2 initialPosition = new Vector2(0, 0);
        Vector2 initialVelocity = new Vector2(0, 0);
        transform.position = initialPosition;
        rb2d.velocity = initialVelocity;
    }
}
