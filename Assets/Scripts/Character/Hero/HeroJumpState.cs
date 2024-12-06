using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJumpState : HeroState
{
    public HeroJumpState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, hero.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(rb.velocity.y < 0  )
            stateMachine.ChangeState(hero.airState);
    }
}
