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
        var pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        var colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            HeroKnight hero = colInfo.GetComponent<HeroKnight>();
            if (hero != null)
            {
                // Debug.Log("Player detected by BossWeapon"); // Debug log
                colInfo.GetComponent<HeroKnight>().Hurt(attackDamage);
            }
            else
            {
                // Debug.LogWarning("Detected collider '" + colInfo.name + "' does not have a HeroKnight component.");
            }
        }
        else{
            // Debug.LogWarning("No target in range");
        }
   }

    private void OnDrawGizmosSelected()
    {
        var pos = transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
