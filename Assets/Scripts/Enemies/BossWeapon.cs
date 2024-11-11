using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
   public int attackDamage = 4;
   public int enragedAttackDamage = 6;

   public Vector3 attackOffset;
   public float attackRange = 1f;
   public LayerMask attackMask;

   public void Attack()
   {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            Debug.Log("Player detected by BossWeapon"); // Debug log
            colInfo.GetComponent<CharacterHealth>().TakeDamage(attackDamage);
        }
        else{
            Debug.LogWarning("No target in range");
        }
   }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
