using System.Collections;
using UnityEngine;

public class HeroDamage : MonoBehaviour
{
    public int damage = 1;
    public HeroKnight knight;
    private Color originalColor;
    public float colorChangeDuration = 0.5f; // Time the skeleton stays red
    public float damageCooldown = 1.0f; // Time between applying damage
    private float lastDamageTime = -Mathf.Infinity; // Last time damage was applied

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Get the CharacterHealth component on the enemy
            CharacterHealth enemyHealth = collision.GetComponent<CharacterHealth>();
            if (enemyHealth != null && knight.Getm_swordEnabled())
            {
                // Start color change coroutine to make the enemy flash red
                StartCoroutine(ChangeEnemyColor(collision.gameObject));

                // Apply damage only if cooldown has passed
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    enemyHealth.TakeDamage(damage);
                    lastDamageTime = Time.time; // Update last damage time
                    Debug.Log("Enemy hit for damage!");

                    // Flip the enemy
                    FlipEnemy(collision.gameObject);
                }
            }
        }
    }

    private IEnumerator ChangeEnemyColor(GameObject enemy)
    {
        SpriteRenderer enemyRenderer = enemy.GetComponent<SpriteRenderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.color; // Save original color
            enemyRenderer.color = Color.red; // Change to red
            yield return new WaitForSeconds(colorChangeDuration);
            enemyRenderer.color = originalColor; // Revert to original color
        }
    }

    private void FlipEnemy(GameObject enemy)
    {
        // Get the EnemyPatrol script from the enemy
        EnemyPatrol enemyPatrol = enemy.GetComponent<EnemyPatrol>();
        if (enemyPatrol != null)
        {
            // Flip the enemy direction
            enemyPatrol.dir *= -1;

            // Flip the enemy sprite by inverting the X scale
            Vector3 scale = enemy.transform.localScale;
            scale.x *= -1;
            enemy.transform.localScale = scale;
        }
    }
}
