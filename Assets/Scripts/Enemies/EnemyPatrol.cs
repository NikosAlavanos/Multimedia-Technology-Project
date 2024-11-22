using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 0.8f;
    public float range = 3;

    float startingX;
    public int dir = 1;

    void Start()
    {
        startingX = transform.position.x;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        // Move the enemy
        transform.Translate(Vector2.right * (speed * Time.deltaTime * dir));

        // Check if the enemy has reached the patrol limits
        if (!(transform.position.x < startingX) && !(transform.position.x > startingX + range)) return;
        dir *= -1;

        // Flip the enemy by inverting the X scale
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
