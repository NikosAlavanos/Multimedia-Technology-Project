using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public LayerMask groundLayer; // Layer for ground
    public LayerMask wallLayer; // Layer for walls
    public Transform groundPoint; // Check point for ground detection
    public Transform wallPoint; // Check point for wall detection
    public float distance = 1f; // Distance to check for ground and walls
    private bool facingRight = false;

    public float flipCooldown = 1f; // Time in seconds before flipping is allowed again
    private float lastFlipTime = 0f; // Tracks the last time a flip occurred

    void Update()
    {
        // Skip patrol flip check if within cooldown
        if (Time.time - lastFlipTime >= flipCooldown)
        {
            // Move the enemy
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Raycast to check for ground below
            RaycastHit2D groundHitInfo = Physics2D.Raycast(groundPoint.position, Vector2.down, distance, groundLayer);

            // Raycast to check for walls in front
            RaycastHit2D wallHitInfo = Physics2D.Raycast(wallPoint.position, facingRight ? Vector2.right : Vector2.left, distance, wallLayer);

            if (wallHitInfo || !groundHitInfo)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        // Toggle facing direction and reset cooldown timer
        facingRight = !facingRight;
        transform.eulerAngles = facingRight ? new Vector3(0, 0, 0) : new Vector3(0, -180, 0);
        lastFlipTime = Time.time;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check ray
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundPoint.position, Vector2.down * distance);

        // Visualize the wall check ray
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(wallPoint.position, facingRight ? Vector2.left * distance : Vector2.right * distance);
    }
}
