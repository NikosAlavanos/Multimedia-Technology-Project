using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCounterAttackState : HeroState
{
    public HeroCounterAttackState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = hero.counterAttackDuration;
        hero.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //hero.SetZeroVelocity();//should make this method so he can't counter while moving

        Collider2D[] colliders = Physics2D.OverlapCircleAll(hero.attackCheck.position, hero.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy3>() != null)
            {
                if (hit.GetComponent<Enemy3>().CanBeStunned())//TODO: probably change it to the right script of the enmy
                {
                    stateTimer = 11;
                    hero.anim.SetBool("SuccessfulCounterAttack", true);
                }
                //else if (hit.GetComponent<Enemy1>().CanBeStunned())
                //{
                //    stateTimer = 11;
                //    hero.anim.SetBool("SuccessfulCounterAttack", true);
                //}
                //else if (hit.GetComponent<Enemy2>().CanBeStunned())
                //{
                //    stateTimer = 11;
                //    hero.anim.SetBool("SuccessfulCounterAttack", true);
                //}
            }
        }

        if(stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(hero.idleState);
    }
}
