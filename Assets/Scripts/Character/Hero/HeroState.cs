using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState
{
    protected HeroStateMachine stateMachine;
    protected Hero hero;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public HeroState(Hero _hero, HeroStateMachine _stateMachine, string _animBoolName)
    {
        this.hero = _hero;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        hero.anim.SetBool(animBoolName, true);
        rb = hero.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        hero.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        hero.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
