using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy //inhereting from the enemy
{
    private BoxCollider2D cd;

    protected override void Awake()
    {
        base.Awake();

        cd = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();

        anim.SetFloat("xVelocity", rb.velocity.x);

        if(isDead)
            return;

        HandleMovement();
        HandleCollision();

        if(isGrounded)
            HandleTurnAround();
    }

    private void HandleTurnAround()
    {
        if (!isGroundInfrontDetected || isWallDetected)
        {
            if (isGrounded == false)
                return;

            Flip();
            idleTimer = idleDuration; //everytime we Flip 
            rb.velocity = Vector2.zero; //stoping his velocity once he stops
        }
    }

    private void HandleMovement()
    {
        if(idleTimer > 0) //every time we flop mushroom will wait
            return;
        
        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);
    }

    public override void Die()
    {
        base.Die();

        cd.enabled = false;
    }
}
