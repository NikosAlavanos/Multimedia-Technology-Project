using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationTriggers : MonoBehaviour
{
    private Hero hero => GetComponentInParent<Hero>();

    private void AnimationTrigger()
    {
        hero.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hero.attackCheck.position, hero.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy3>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                    hero.stats.DoDamage(_target);

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                if (weaponData != null)
                    weaponData.Effect(_target.transform);
            }
        }
    }
}
