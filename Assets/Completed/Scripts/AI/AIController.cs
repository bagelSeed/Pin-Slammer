using UnityEngine;
using System.Collections;

public class AIController : TinPinObjectController {

    [HideInInspector]public int level;
    public CompletePlayerController player;
    [HideInInspector]public FSM fsm;
    [HideInInspector]public bool busy;
    private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.

    bool AIDisabled = false;

    // Use this for initialization
    void Start () {
        fsm = new FSM( this );
        level = 1;
        busy = false;

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
        if (player == null)
            player = GetComponent<CompletePlayerController>();
        if (player != null)
        {
            if (fsm != null)
                fsm.Update();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = rb2d.velocity.x;
        float moveVertical = rb2d.velocity.y;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        float friction_ = AIDisabled ? 1 : (1 - friction / 10);
        rb2d.velocity = movement * friction_;
    }

    private void OnTriggerEnter(Collider player)
    {
        Debug.Log("AI Collided With Player!");
    }

    // Public function
    public void AttackPlayer()
    {
        Vector3 direction = player.transform.position;
    }
}
