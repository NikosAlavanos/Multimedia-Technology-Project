using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWallSlideState : HeroState
{
    public HeroWallSlideState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
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

        if(hero.IsWallDetected() == false)
            stateMachine.ChangeState(hero.airState);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(hero.wallJump);
            return;
        }

        if (xInput != 0 && hero.facingDir != xInput)
            stateMachine.ChangeState(hero.idleState);
        
        if(yInput < 0)
            rb.velocity = new Vector2(0,rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if(hero.IsGroundDetected())
            stateMachine.ChangeState(hero.idleState);
    }
}
