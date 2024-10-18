using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("takeDamage");

        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }


    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Enemy died!");
    }
}