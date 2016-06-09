using UnityEngine;
using System.Collections;
using System;

public interface State {
    // We may want to update something on enter, like our delta time
    void UpdateState();
    void OnTriggerEnter(Collider other);

    // Current States:
    void ToPatrolState();
    void ToAttackState();
    void ToChaseState();
}

public class Attack : State
{
    private readonly AIController AI;
    public Attack(AIController AI_)
    {
        AI = AI_;
    }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider other) { }

    public void ToPatrolState() { }

    public void ToAttackState() { }

    public void ToChaseState() { }
}

public class Escape : State
{
    private readonly AIController AI;
    public Escape(AIController AI_)
    {
        AI = AI_;
    }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider other) { }

    public void ToPatrolState() { }

    public void ToAttackState() { }

    public void ToChaseState() { }
}

public class Patrol : State
{
    private readonly AIController AI;
    public Patrol(AIController AI_)
    {
        AI = AI_;
    }

    public void UpdateState()
    {
        // Want to check if we should attack user
        // Check if we have the reflex (The time passed from previous state)
        // Switch state, or continue with this state

        AI.AttackPlayer();
        ToAttackState();
    }

    public void OnTriggerEnter(Collider other) { }

    public void ToPatrolState() { }

    public void ToAttackState() {
    }

    public void ToChaseState() { }
}