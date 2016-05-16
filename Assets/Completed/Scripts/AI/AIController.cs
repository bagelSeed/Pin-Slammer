using UnityEngine;
using System.Collections;

public class AIController : TinPinObjectController {

    [HideInInspector]public int level;
    [HideInInspector]public CompletePlayerController player;
    [HideInInspector]public FSM fsm;
    [HideInInspector]public bool busy;

	// Use this for initialization
	public void createAI (CompletePlayerController player_) {
        fsm = new FSM( this );
        player = player_;
        level = 1;
        busy = false;

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
        if (fsm != null)
            fsm.Update();
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
