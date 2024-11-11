using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public Enemy health;
    private EnemyPatrol patrolScript;

    private void Start()
    {
        patrolScript = GetComponent<EnemyPatrol>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HeroKnight hero = collision.gameObject.GetComponent<HeroKnight>();
            if (hero != null && !hero.IsBlocking)
            {
                health.TakeDamage(damage);
            }

            // Flip the enemy direction
            FlipEnemyDirection();
        }
    }

    private void FlipEnemyDirection()
    {
        // Reverse the patrol direction
        patrolScript.dir *= -1;

        // Flip the enemy by inverting the X scale
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
