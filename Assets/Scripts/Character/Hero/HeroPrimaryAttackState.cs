using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPrimaryAttackState : HeroState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2f;//how much time should pass before combo resets

    public HeroPrimaryAttackState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        hero.anim.SetInteger("ComboCounter", comboCounter);
        //hero.anim.speed = 1.2f;//attackspeed, 1 is the base


        float attackDir = hero.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        hero.SetVelocity(hero.attackMovement[comboCounter].x * attackDir, hero.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        hero.StartCoroutine("BusyFor", .15f);
        //hero.anim.speed = 1;//attackspeed returns to normal

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
            hero.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(hero.idleState);
    }
}
