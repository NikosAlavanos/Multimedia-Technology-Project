using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy3 enemy;//TODO: change the script

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy3>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();

        enemy.Die();
    }
}