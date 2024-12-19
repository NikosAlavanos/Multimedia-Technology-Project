using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : HeroGroundedState
{
    public HeroIdleState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        hero.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput == hero.facingDir && hero.IsWallDetected())
            return;

        if(xInput != 0 && !hero.isBusy)
            stateMachine.ChangeState(hero.moveState);
    }
}
