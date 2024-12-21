using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy {  get; private set; }

    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Roll info")]
    public float rollSpeed;
    public float rollDuration;
    public float rollDir {  get; private set; }

    public SkillManager skill { get; private set; }
    #region States
    public HeroStateMachine stateMachine { get; private set; }

    public HeroIdleState idleState { get; private set; }

    public HeroMoveState moveState { get; private set; }
    public HeroJumpState jumpState { get; private set; }
    public HeroAirState airState { get; private set; }
    public HeroWallSlideState wallSlide { get; private set; }
    public HeroWallJumpState wallJump { get; private set; }
    public HeroRollState rollState { get; private set; }

    public HeroPrimaryAttackState primaryAttack { get; private set; }
    public HeroCounterAttackState counterAttack { get; private set; }
    public HeroDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new HeroStateMachine();

        idleState = new HeroIdleState(this, stateMachine, "Idle");
        moveState = new HeroMoveState(this, stateMachine, "Move");
        jumpState = new HeroJumpState(this, stateMachine, "Jump");
        airState  = new HeroAirState(this, stateMachine, "Jump");
        rollState = new HeroRollState(this, stateMachine, "Roll");
        wallSlide = new HeroWallSlideState(this, stateMachine, "WallSlide");
        wallJump  = new HeroWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new HeroPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new HeroCounterAttackState(this, stateMachine, "CounterAttack");

        deadState = new HeroDeadState(this, stateMachine, "Death");
    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckForRollInput();

        if (Input.GetKeyDown(KeyCode.Q))//TODO: comment it out
            stateMachine.ChangeState(deadState);
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForRollInput()
    {
        if (IsWallDetected())
            return;

        

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.roll.CanUseSkill())
        {
            
            rollDir = Input.GetAxisRaw("Horizontal");

            if (rollDir == 0)
                rollDir = facingDir;

            stateMachine.ChangeState(rollState);
        }
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
