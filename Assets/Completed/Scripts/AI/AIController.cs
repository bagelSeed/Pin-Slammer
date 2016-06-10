using UnityEngine;
using System.Collections;

public class AIController : TinPinObjectController {

    [HideInInspector]public int level;
    public CompletePlayerController player;
    [HideInInspector]public FSM fsm;
    [HideInInspector]public bool busy;
    private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

    private float bufferTime=0f;

    // Use this for initialization
    void Start () {
        fsm = new FSM( this );
        level = 1;
        busy = false;
        lives = 3;

        rb2d = GetComponent<Rigidbody2D>();

        switch (level)
        {
            case 1:
                {
                    break;
                }
            case 2:
                {
                    // Take caution if player is circling around, or using special items
                    break;
                }
            case 3:
                {
                    // Taunt, use item percisely when user is haulting. etc
                    break;
                }
        }
    }
	
	// Update is called once per frame
	void Update () {
        bufferTime += Time.deltaTime;
        if (player == null)
            player = GetComponent<CompletePlayerController>();
        if (player != null && fsm.getBufferTime() > bufferTime)
        {
            Debug.Log("Buffer: " + bufferTime);
            bufferTime = 0;
            if (fsm != null)
                fsm.Update();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = rb2d.velocity.x;
        float moveVertical = rb2d.velocity.y;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        float friction_ = playerDisabled ? 1 : (1 - friction / 10);
        rb2d.velocity = movement * friction_;

        if (playerDisabled)
        {
            if (transform.localScale.x < shrinkScale &&
                transform.localScale.y < shrinkScale &&
                transform.localScale.z < shrinkScale)
            {
                // Take AI Lives
                --lives;
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
        Vector2 initialPosition = new Vector2(0, 7f);
        Vector2 initialVelocity = new Vector2(0, 0);

        transform.position = initialPosition;
        transform.localScale = Vector3.one * initialScale;
        rb2d.velocity = initialVelocity;
    }

    // Public function
    public void AttackPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude > 7)
        {
            direction = direction.normalized * 7;
        }
        Vector2 movement = direction * speed * percentage;
        rb2d.AddForce(movement);
    }
}
