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
}
