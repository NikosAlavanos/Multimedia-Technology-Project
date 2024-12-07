using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    public Transform player;

    public bool isFlipped = false;
    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float colorChangeDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        
        // Cache the sprite renderer and original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");
        
        // // If the enemy's name is "skeleton", change its color to red
        // if (gameObject.name.ToLower().Contains("skeleton") && spriteRenderer != null)
        // {
        //     StartCoroutine(FlashRed());
        // }

        // Every enemy that is being damaged will flash red
        StartCoroutine(FlashRed());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    IEnumerator FlashRed()
    {
        // Change color to red
        spriteRenderer.color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(colorChangeDuration);

        // Revert to original color
        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }


    public void FlipPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        transform.localScale = flipped;
        transform.Rotate(0f, 180f, 0f);
    }


    public void LookAtPosition(Vector2 pos)
    {
        if (transform.position.x > pos.x && isFlipped)
        {
            
            FlipPlayer();
            isFlipped = false;
        }
        else if (transform.position.x < pos.x && !isFlipped)
        {
            FlipPlayer();
            isFlipped = true;
        }
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x && isFlipped)
        {
            
            FlipPlayer();
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            FlipPlayer();
            isFlipped = true;
        }
    }
}
