using UnityEngine;

public class HeroDamage : MonoBehaviour
{
    public int damage = 1;
    public HeroKnight knight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Get the CharacterHealth component on the enemy and deal damage
            CharacterHealth enemyHealth = collision.GetComponent<CharacterHealth>();
            if (enemyHealth != null && knight.Getm_swordEnabled())
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("Enemy hit for damage!");
                // Optionally destroy the enemy if it should be removed immediately
                // Destroy(collision.gameObject);
            }
        }
    }

    //public void EnableSwordCollider()
    //{
    //    // Enable the sword collider during the attack animation
    //    GetComponent<Collider2D>().enabled = true;
    //}

    //public void DisableSwordCollider()
    //{
    //    // Disable the sword collider after the attack animation
    //    GetComponent<Collider2D>().enabled = false;
    //}
}
