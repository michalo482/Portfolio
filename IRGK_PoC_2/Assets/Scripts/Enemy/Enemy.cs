using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Rigidbody Rb { get; private set; }
    public Animator Animator { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
