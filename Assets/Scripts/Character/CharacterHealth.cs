using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    public Character character;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        character.Hurt();

        if (health <= 0)
        {
            character?.Death();
            if (character.CompareTag("Enemy")) Destroy(gameObject);
            // Destroy(gameObject); // Destroy the enemy GameObject
        }
    }
}
