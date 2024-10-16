using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour

{
    [SerializeField]private int maxHealth = 100;
    public int currentHealth;
    Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }


    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
