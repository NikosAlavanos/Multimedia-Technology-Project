using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAirState : HeroState
{
    public HeroAirState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
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

        if (hero.IsWallDetected())
            stateMachine.ChangeState(hero.wallSlide);

        if(hero.IsGroundDetected())
            stateMachine.ChangeState(hero.idleState);

        if (xInput != 0)
            hero.SetVelocity(hero.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}