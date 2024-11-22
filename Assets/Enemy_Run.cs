using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float playerDetectionRange = 5f;
    public float groundCheckDistance = 1f;
    public LayerMask groundLayer;
    
    Transform player;
    Rigidbody2D rb;
    Enemy enemy;

    // Distance the enemy will move from the start point
    public float patrolDistance = 5f;
    // Patrol speed
    public float patrolSpeed = 2f;
    // Starting position of the enemy
    private Vector2 startPosition;
    // Target position
    private Vector2 targetPositionPatrol;
    // Track direction of the movement
    private bool movingToLeft = true;
    // Check if the starting position has been initialized
    private bool spInitialized;
    // Flag to track if enemy is on ground
    private bool isGrounded = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the enemy item through animator
        enemy = animator.GetComponent<Enemy>();
        // Get the enemies position
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Get the rigid body of the enemy
        rb = animator.GetComponent<Rigidbody2D>();

        if (spInitialized) return;
        // Record the starting position of the enemy
        startPosition = rb.position;
        // Calculate the patrol area of the enemy
        targetPositionPatrol = startPosition + Vector2.left * patrolDistance;
        // Starting position has been initialized
        spInitialized = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isGrounded = GroundDetected();

        ChaseOrPatrol(animator);
    }

    private void ChaseOrPatrol(Animator animator)
    {
        // If the enemy is not grounded or is near the edge, continue patrolling
        if (!isGrounded || IsNearEdge())
        {
            Patrol(animator);
            return;
        }

        // Check if the player is within the detection range
        if (Mathf.Abs(rb.position.x - player.position.x) < playerDetectionRange)
        {
            enemy.LookAtPlayer();
            ChasePlayer();
        }
        else
        {
            Patrol(animator);
        }
    }

    private void ChasePlayer()
    {
        // Target position that we want to move to
        var target = new Vector2(player.position.x, rb.position.y);
        var newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    private void Patrol(Animator animator)
    {
        var patrolMoveTo = movingToLeft ? targetPositionPatrol : startPosition;
        var newPos = Vector2.MoveTowards(rb.position, patrolMoveTo, patrolSpeed * Time.fixedDeltaTime);
        // Make the enemy look at the patrol position
        enemy.LookAtPosition(patrolMoveTo);
        rb.MovePosition(newPos);

        // Check if we've reached the patrol target position
        if (Vector2.Distance(rb.position, patrolMoveTo) < 0.1f)
        {
            movingToLeft = !movingToLeft;
        }
    }
    
    private bool GroundDetected()
    {
        // Get the enemy's bounds
        var enemyBounds = enemy.GetComponent<Collider2D>().bounds;
        
        // Calculate the center of the enemy's bounds
        Vector2 center = enemyBounds.center;

        // Calculate an offset based on half the height of the bounds
        var offsetY = enemyBounds.size.y / 2f;

        // Calculate the bottom center point for the raycast
        // * -1f to position the ray at the feet of the enemy
        var bottomCenter = center + new Vector2(0f, offsetY * -1f);

        // Perform the raycast
        var groundHit = Physics2D.Raycast(bottomCenter, Vector2.down, groundCheckDistance, groundLayer);

        // Draw the raycast for debugging
        Debug.DrawRay(bottomCenter, Vector2.down * groundCheckDistance, Color.red);
        return groundHit;
    }
    
    private bool IsNearEdge()
    {
        // Get the enemy's bounds
        var enemyBounds = enemy.GetComponent<Collider2D>().bounds;

        // Calculate the direction the enemy is facing
        var direction = enemy.isFlipped ? -1f : 1f;

        // Calculate the front point based on the facing direction
        var frontPoint = new Vector2(enemyBounds.center.x + direction * enemyBounds.extents.x, enemyBounds.min.y);

        // Perform a raycast to check for ground ahead
        var edgeHit = Physics2D.Raycast(frontPoint, Vector2.down, groundCheckDistance, groundLayer);

        // Draw the ray for debugging
        Debug.DrawRay(frontPoint, Vector2.down * groundCheckDistance, Color.blue);

        // Return true if no ground was detected
        return !edgeHit;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
