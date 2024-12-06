using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGroundedState : HeroState
{
    public HeroGroundedState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse0))//GetKey,GetKeyUp
            stateMachine.ChangeState(hero.primaryAttack);

        if(!hero.IsGroundDetected())
            stateMachine.ChangeState(hero.airState);
        
        if (Input.GetKeyDown(KeyCode.Space) && hero.IsGroundDetected())
            stateMachine.ChangeState(hero.jumpState);
    }
}
