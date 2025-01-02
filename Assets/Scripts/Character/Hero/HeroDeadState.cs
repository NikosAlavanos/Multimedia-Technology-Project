using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeadState : HeroState
{
    public HeroDeadState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName) : base(_hero, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("HeroDeadState Enter Called");

        DeathMenuManager deathMenu = GameObject.FindObjectOfType<DeathMenuManager>();
        if (deathMenu != null)
        {
            Debug.Log("DeathMenu Found, Showing Menu");
            deathMenu.ShowDeathMenuManager();
        }
        else
        {
            Debug.LogError("DeathMenu Not Found in Scene");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        hero.SetZeroVelocity();
    }
}
