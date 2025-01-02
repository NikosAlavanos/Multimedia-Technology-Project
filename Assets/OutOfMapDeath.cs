using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapDeath : MonoBehaviour
{
    public int damageAmount = 1000; // Damage to apply
    public Vector3 damageOffset;   // Offset for the damage area
    public float damageRange = 1f; // Range of the damage zone
    public LayerMask targetMask;   // Layer mask for the target

    private void Update()
    {
        // Constantly check for targets in the damage zone
        ApplyDamage();
    }

    private void ApplyDamage()
    {
        // Calculate the position of the damage zone
        var pos = transform.position + transform.right * damageOffset.x + transform.up * damageOffset.y;

        // Detect colliders within the damage zone
        var colInfo = Physics2D.OverlapCircle(pos, damageRange, targetMask);

        if (colInfo != null)
        {
            // Check if the collider has the CharacterStats component
            CharacterStats targetStats = colInfo.GetComponent<CharacterStats>();

            if (targetStats != null)
            {
                // Apply damage using the TakeDamage method
                Debug.Log($"Hero detected: {colInfo.name}, applying {damageAmount} damage.");
                targetStats.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning($"Collider '{colInfo.name}' does not have CharacterStats.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the damage zone in the editor
        var pos = transform.position + transform.right * damageOffset.x + transform.up * damageOffset.y;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, damageRange);
    }
}