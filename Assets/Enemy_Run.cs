using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float playerDetectionRange = 5f;

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
    private bool spInitialized = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();

        if (!spInitialized)
        {
            // Record the starting position of the enemy
            startPosition = rb.position;
            targetPositionPatrol = startPosition + Vector2.left * patrolDistance;
            spInitialized = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the player is within the detection range of the skeleton
        if (Mathf.Abs(rb.position.x - player.position.x) < playerDetectionRange)
        {
            enemy.LookAtPlayer();
            // Target position that we want to move to
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            Vector2 patrolMoveTo = movingToLeft ? targetPositionPatrol : startPosition;
            Vector2 newPos = Vector2.MoveTowards(rb.position, patrolMoveTo, patrolSpeed * Time.fixedDeltaTime);
            // Make the enemy look at the patrol position
            enemy.LookAtPosition(patrolMoveTo);
            rb.MovePosition(newPos);

            // Check if we've reached the patrol target position
            if (Vector2.Distance(rb.position, patrolMoveTo) < 0.1f)
            {
                movingToLeft = !movingToLeft;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    
}
