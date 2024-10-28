using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public CharacterHealth health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HeroKnight hero = collision.gameObject.GetComponent<HeroKnight>();
            if (hero != null && !hero.IsBlocking)
            {
                health.TakeDamage(damage);
            }
            
        }
    }
}
