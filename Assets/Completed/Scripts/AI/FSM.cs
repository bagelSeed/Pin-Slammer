using UnityEngine;
using System.Collections;

public class FSM {

    public AIController AI;

    [HideInInspector]public State currState;
    [HideInInspector]public Attack attackState;
    [HideInInspector]public Escape escapeState;
    [HideInInspector]public Patrol patrolState;

    private void Awake()
    {
        attackState = new Attack(AI);
        escapeState = new Escape(AI);
        patrolState = new Patrol(AI);
    }

    public FSM (AIController AI_)
    {
        AI = AI_;

        if (patrolState == null)
            Awake();
        currState = patrolState;
    }

    // Update is called once per frame
    public void Update()
    {

        if (currState != null)
            currState.UpdateState();
    }
}

