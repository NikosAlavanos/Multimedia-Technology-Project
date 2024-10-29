using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public CharacterHealth health;
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
            patrolScript.Flip();
        }
    }
}
