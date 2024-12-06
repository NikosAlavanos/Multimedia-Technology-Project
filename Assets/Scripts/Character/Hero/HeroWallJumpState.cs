using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWallJumpState : HeroState
{
    public HeroWallJumpState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f;
        hero.SetVelocity(5 * -hero.facingDir, hero.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(hero.airState);

        if(hero.IsGroundDetected())
            stateMachine.ChangeState(hero.idleState);
    }
}
