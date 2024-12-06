using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRollState : HeroState
{
    public HeroRollState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = hero.rollDuration;

    }

    public override void Exit()
    {
        base.Exit();

        hero.SetVelocity(0,rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!hero.IsGroundDetected() && hero.IsWallDetected())
            stateMachine.ChangeState(hero.wallSlide);

        hero.SetVelocity(hero.rollSpeed * hero.rollDir, rb.velocity.y); //rb.velocity.y == 0 if we dont want to fall while rolling

        if (stateTimer < 0)
            stateMachine.ChangeState(hero.idleState);
    }
}
