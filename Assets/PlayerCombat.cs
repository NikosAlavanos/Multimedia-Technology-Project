using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (!(Time.time >= nextAttackTime)) return;
        if (!Input.GetMouseButtonDown(0)) return;
        Attack();
        nextAttackTime = Time.time + 1f / attackRate;
    }

    public void Attack()
    {
        // Detect enemies in range of attack
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // Damage them
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy1>().TakeDamage(attackDamage);
        }
    }

    
}
