using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveState : HeroGroundedState
{
    public HeroMoveState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        hero.SetVelocity(xInput * hero.moveSpeed, rb.velocity.y);

        if(xInput == 0 || hero.IsWallDetected())
            stateMachine.ChangeState(hero.idleState);
    }
}
