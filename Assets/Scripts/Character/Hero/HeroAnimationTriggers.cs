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
            if (hit.GetComponent<Enemy3>() != null)//TODO: probably change it to the right script of the enmy
                hit.GetComponent<Enemy3>().Damage();
            else if(hit.GetComponent<Enemy1>() != null)
                hit.GetComponent<Enemy1>().Damage();
            else if(hit.GetComponent<Enemy2>() != null)
                hit.GetComponent<Enemy2>().Damage();
        }
    }
}
