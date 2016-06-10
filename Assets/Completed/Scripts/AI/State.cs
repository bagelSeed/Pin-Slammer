using UnityEngine;
using System.Collections;
using System;

public interface State {
    // We may want to update something on enter, like our delta time
    void UpdateState();
    void OnTriggerEnter(Collider other);

    // Current States:
    float getBufferTime();
}

public class Attack : State
{
    private readonly AIController AI;
    public Attack(AIController AI_)
    {
        AI = AI_;
    }

    public float getBufferTime() { return 1.5f; }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider other) { }
}

public class Escape : State
{
    private readonly AIController AI;
    public Escape(AIController AI_)
    {
        AI = AI_;
    }

    public float getBufferTime() { return 2.0f; }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider other) { }
}

public class Patrol : State
{
    private readonly AIController AI;
    public Patrol(AIController AI_)
    {
        AI = AI_;
    }

    public float getBufferTime() { return 2.0f; }

    public void UpdateState()
    {
        // Want to check if we should attack user
        // Check if we have the reflex (The time passed from previous state)
        // Switch state, or continue with this state

        AI.AttackPlayer();
        AI.fsm.ToAttackState();
    }

    public void OnTriggerEnter(Collider other) { }
}