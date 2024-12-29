using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Heal effect", menuName = "Data/Item effect/Heal effect")]

public class Heal_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercentage;

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        HeroStats heroStats = HeroManager.instance.hero.GetComponent<HeroStats>();

        int healAmount = Mathf.RoundToInt(heroStats.GetMaxHealthValue() * healPercentage);

        heroStats.IncreaseHealthBy(healAmount);
    }
}
