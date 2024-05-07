using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    
    protected bool triggerCalled;
    private string _animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = enemyStateMachine;
        this._animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemy.Animator.SetBool(_animBoolName, true);
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        enemy.Animator.SetBool(_animBoolName, false);
    }
}
