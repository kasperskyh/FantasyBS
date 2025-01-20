using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;
    public Boolean isDead = false;

    [SerializeField] private float knockbackForce = 5f; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage, Vector2 damageSourcePosition)
    {
        currentHealth -= damage;
        animator.SetTrigger("takeDamage");

        ApplyKnockback(damageSourcePosition);

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }
    }

    private void ApplyKnockback(Vector2 damageSourcePosition)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - damageSourcePosition;
        knockbackDirection.Normalize();

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy died!");
    }
}
