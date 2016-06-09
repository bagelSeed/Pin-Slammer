using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class CompletePlayerController : TinPinObjectController {

    [HideInInspector]public STATUS currStatus;
    private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

    LineRenderer lineRenderer;
    SpriteRenderer forceArrow;

    public Camera camera_;

    Vector2 direction;

    // Use this for initialization
    void Start()
	{
        camera_ = Camera.main;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Player";
        lineRenderer.sortingOrder = 1;

        forceArrow = GetComponent<SpriteRenderer>();
        forceArrow.sortingLayerName = "Player";
        forceArrow.sortingOrder = 1;

        rb2d = GetComponent<Rigidbody2D> ();

        spawnPlayer();
    }

    void OnMouseDrag()
    {
        if (playerDisabled) return;

        lineRenderer.SetVertexCount(2);

        direction = transform.position - camera_.ScreenToWorldPoint(Input.mousePosition);

        if (direction.magnitude > 7)
        { 
            direction = direction.normalized * 7;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector2(transform.position.x + direction.x, transform.position.y + direction.y));
    }

    void OnMouseUp()
    {
        if (playerDisabled) return;

        // World pos - world pos
        Vector2 movement = direction * speed * percentage;
        rb2d.AddForce(movement);
        lineRenderer.SetVertexCount(0);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
	{
        float moveHorizontal = rb2d.velocity.x;
        float moveVertical = rb2d.velocity.y;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        float friction_ = playerDisabled ? 1 : (1 - friction / 10);
        rb2d.velocity = movement * friction_;

        // Set Status
        Vector3 mag = rb2d.velocity;
        if (mag.magnitude < 5)
            currStatus = STATUS.HAULTED;
        else if (mag.magnitude < 15)
            currStatus = STATUS.PATROLLING;
        else
        {
            // a different way of determining if player is attack if AI level > 2
            currStatus = STATUS.ATTACKING;
        }

        if (playerDisabled && lives > 0)
        {
            if (transform.localScale.x < shrinkScale &&
                transform.localScale.y < shrinkScale &&
                transform.localScale.z < shrinkScale)
            {
                lives--;
                playerDisabled = lives == 0;
                if (playerDisabled)
                {
                    transform.localScale = Vector3.zero;
                    return;
                }
                spawnPlayer();
            }
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag("Background"))
        {
            playerDisabled = true;
        }
	}

    void spawnPlayer()
    {
        // Eventually get some kind of init position getting for each map
        Vector2 initialPosition = new Vector2(0, -7f);
        Vector2 initialVelocity = new Vector2(0, 0);

        transform.position = initialPosition;
        transform.localScale = Vector3.one * initialScale;
        rb2d.velocity = initialVelocity;
    }
}
