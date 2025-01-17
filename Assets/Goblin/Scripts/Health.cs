using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private double maxHealth = 100;
    public double currentHealth;
    Animator animator;
    Vector2 startPos;

    [Header("iframes")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private float iFrameDeltaTime;
    [SerializeField] private int flashCount;
    private SpriteRenderer spriteRend;
    private bool isInvincible = false;

    [Header("Dash Settings")]
    [SerializeField] private float dashInvincibilityDuration = 1f; // Czas nie�miertelno�ci po dashu
    private bool isDashing = false;

    void Start()
    {
        startPos = transform.position;
        currentHealth = maxHealth;
        iFrameDuration = 2f;
        iFrameDeltaTime = 0.2f; // Czas przerwy mi�dzy migni�ciami
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(double damage)
    {
        if (isInvincible) return; // Zignoruj obra�enia, je�li posta� jest nie�miertelna

        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
        StartCoroutine(Invulnerability());
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Player healed! Current health: " + currentHealth);
    }

    public void Die()
    {
       Respawn();
    }

    private void Respawn()
    {
        transform.position=startPos;
        currentHealth=100;
        animator.SetBool("isDead", false);
    }

    private IEnumerator Invulnerability()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (int i = 0; i < flashCount; i++)
        {
            // Wy��cz sprite, aby posta� znikn�a
            spriteRend.enabled = false;
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

            // W��cz sprite, aby posta� si� pojawi�a
            spriteRend.enabled = true;
            yield return new WaitForSeconds(iFrameDeltaTime / 2);
        }

        Debug.Log("Player is no longer invincible!");
        isInvincible = false;
    }

    public void Dash()
    {
        if (isDashing) return;

        StartCoroutine(PerformDash());
    }

    private IEnumerator PerformDash()
    {
        Debug.Log("Dash started!");
        isDashing = true;
        isInvincible = true; // W��cz nie�miertelno�� na czas dasha

        // Symulacja czasu trwania dasha (1 sekunda)
        yield return new WaitForSeconds(dashInvincibilityDuration);

        Debug.Log("Dash ended, invincibility turned off!");
        isInvincible = false; // Wy��cz nie�miertelno�� po dashu
        isDashing = false;
    }

    private void Update()
    {
        // Obs�uga wci�ni�cia klawisza dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }
}
