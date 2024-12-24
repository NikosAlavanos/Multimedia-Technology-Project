using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : CharacterStats
{
    private Hero hero;

    protected override void Start()
    {
        base.Start();

        hero = GetComponent<Hero>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();

        hero.Die();
    }
}
