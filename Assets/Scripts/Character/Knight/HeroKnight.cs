﻿using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public int maxHealth = 100;
    public int currentHealth;

   
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    private bool m_isDead = false;
    private bool m_swordEnabled = false;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _spriteRenderer1;
    public bool IsBlocking => m_animator.GetBool("IdleBlock");


    public bool Getm_swordEnabled()
    { 
        return m_swordEnabled;
    }

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (m_isDead) return;

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if (m_rolling)
        {
            m_rollCurrentTime += Time.deltaTime;

            // Disable rolling if timer extends duration
            if (m_rollCurrentTime > m_rollDuration + 0.3) // Add some extra seconds to m_rollDuration to not stuck bellow enemy
            {
                // // Check if the player is near any enemies and apply push force
                // Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                // foreach (Collider2D enemy in enemies)
                // {
                //     if (enemy != null && Vector2.Distance(transform.position, enemy.transform.position) < 2.0f) // Check distance threshold (2.0f)
                //     {
                //         Vector2 pushDirection = (transform.position.x > enemy.transform.position.x) ? Vector2.right : Vector2.left;  // Direction away from the enemy
                //         m_body2d.AddForce(pushDirection * m_rollForce  + new Vector2(20, 20), ForceMode2D.Impulse); // Apply a small push force
                //         Debug.Log("Pushing player");
                //         break; // Stop after pushing away from one enemy, you can also loop if you want to apply force for multiple enemies
                //     }
                // }


                // Restore the original layer to re-enable collisions with enemies
                gameObject.layer = LayerMask.NameToLayer("Player");

                // Stop rolling
                m_rolling = false;
            }
        }


        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        var inputX = Input.GetAxis("Horizontal");

        switch (inputX)
        {
            // Swap direction of sprite depending on walk direction
            case > 0:
                _spriteRenderer.flipX = false;
                m_facingDirection = 1;
                break;
            case < 0:
                _spriteRenderer.flipX = true;
                m_facingDirection = -1;
                break;
        }
        
        // Flip attackPoint with player's direction
        attackPoint.localPosition = new Vector3(Mathf.Abs(attackPoint.localPosition.x) * m_facingDirection, 
            attackPoint.localPosition.y, 
            attackPoint.localPosition.z);

        // Move
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }

        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            if (!(Time.time >= nextAttackTime)) return;
            // Disable blocking during attack
            m_animator.SetBool("IdleBlock", false);

            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            
            // Attack the enemy
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Allow blocking to resume only when the player releases the attack button
        else if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButton(1)) m_animator.SetBool("IdleBlock", true);
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");

            // Switch to the NonCollidingLayer to ignore enemy collisions 
            gameObject.layer = LayerMask.NameToLayer("NonCollidingWithEnemies");

            // Apply roll force
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);

            // Reset roll timer
            m_rollCurrentTime = 0.0f;
        }


        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        spawnPosition = m_facingDirection == 1 ? m_wallSensorR2.transform.position : m_wallSensorL2.transform.position;

        if (m_slideDust == null) return;
        // Set correct arrow spawn position
        var dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
        // Turn arrow in correct direction
        dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
    }


    // Animation event for when the attack animation starts
    void AE_StartAttack()
    {
        m_swordEnabled = true;
    }

    // Animation event for when the attack animation ends
    void AE_EndAttack()
    {
        m_swordEnabled = false;
    }

    public void Attack()
    {
        // Deteck enemies in range of attack
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // Damage them
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Draw the range of attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Hurt(int damage)
    {
        if (IsBlocking) return;
        if (m_rolling) return;
        // Play hurt animation
        m_animator.SetTrigger("Hurt");

        // Reduce the life of the HeroKnight
        currentHealth -= damage;

        // If the HeroKnight runs out of health he dies
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        if (m_rolling || m_isDead) return;
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");

        // Set m_isDead to true to prevent further input handling
        m_isDead = true;

        // Disable the player
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
