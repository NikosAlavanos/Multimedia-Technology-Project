using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 40;
    public int enragedAttackDamage = 60;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        // Calculate the position where the attack is happening
        var pos = transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y;

        // Visualize the attack range for debugging
        Debug.Log($"Attack position: {pos}, Range: {attackRange}");

        // Detect colliders within the attack range
        var colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo != null)
        {
            // Check if the collider has the CharacterStats component (the hero)
            CharacterStats targetStats = colInfo.GetComponent<CharacterStats>();

            if (targetStats != null)
            {
                // Apply damage using the TakeDamage method from CharacterStats
                Debug.Log($"Hero detected: {colInfo.name}, applying {attackDamage} damage.");
                targetStats.TakeDamage(attackDamage);  // This applies the damage directly to the hero
            }
            else
            {
                Debug.LogWarning($"Collider '{colInfo.name}' does not have CharacterStats.");
            }
        }
        else
        {
            Debug.LogWarning("No target in range for attack.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        var pos = transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
