using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    public bool isBusy {  get; private set; }

    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Roll info")]
    [SerializeField] private float rollCooldown = 1f;
    private float rollUsageTimer;
    public float rollSpeed = 25f;
    public float rollDuration = 0.2f;
    public float rollDir {  get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    #endregion

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
    #endregion

    private void Awake()
    {
        stateMachine = new HeroStateMachine();

        idleState = new HeroIdleState(this, stateMachine, "Idle");
        moveState = new HeroMoveState(this, stateMachine, "Move");
        jumpState = new HeroJumpState(this, stateMachine, "Jump");
        airState  = new HeroAirState(this, stateMachine, "Jump");
        rollState = new HeroRollState(this, stateMachine, "Roll");
        wallSlide = new HeroWallSlideState(this, stateMachine, "WallSlide");
        wallJump  = new HeroWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new HeroPrimaryAttackState(this, stateMachine, "Attack");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();

        CheckForRollInput();
    }

    IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForRollInput()
    {
        if(IsWallDetected())
            return;

        rollUsageTimer -= Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.LeftShift) && rollUsageTimer < 0)
        {
            rollUsageTimer = rollCooldown;
            rollDir = Input.GetAxisRaw("Horizontal");

            if (rollDir == 0)
                rollDir = facingDir;

            stateMachine.ChangeState(rollState);
        }
    }

    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if(_x < 0 && facingRight)
            Flip();
    }
    #endregion
}
