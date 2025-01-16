using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private double maxHealth = 100;
    public double currentHealth;
    Animator animator;

    [Header("iframes")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float iFrameDeltaTime;
    [SerializeField] private int flashCount;
    private SpriteRenderer spriteRend;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        iFrameDuration = 2f;
        iFrameDeltaTime = 0.2f; // Czas przerwy miêdzy migniêciami
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(double damage)
    {
        if (isInvincible) return;
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
        StartCoroutine(Invulnerability());
    }

    void heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator Invulnerability()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (int i = 0; i < flashCount; i++)
        {
            // Wy³¹cz sprite, aby postaæ zniknê³a
            spriteRend.enabled = false;
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

            // W³¹cz sprite, aby postaæ siê pojawi³a
            spriteRend.enabled = true;
            yield return new WaitForSeconds(iFrameDeltaTime / 2);
        }

        Debug.Log("Player is no longer invincible!");
        isInvincible = false;
    }
}
