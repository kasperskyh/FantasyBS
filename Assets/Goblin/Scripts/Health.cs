using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Health : MonoBehaviour

{
    [Header ("Health")]
    [SerializeField]private double maxHealth = 100;
    public double currentHealth;
    Animator animator;

    [Header ("iframes")]
    [SerializeField]private float iFrameDuration;
    [SerializeField]private float iFrameDeltaTime;
    [SerializeField]private int flashCount;
    //private SpriteRenderer spriteRend;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        iFrameDuration = 2;
        iFrameDeltaTime = 1;
        animator = GetComponent<Animator>();
        
        //spriteRend = GetComponent<SpriteRenderer>();
    }


    public void takeDamage(double damage)
    {
        Movement dash = GetComponent<Movement>();
        if (isInvincible || dash.isDashing) return;
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

    for (float i = 0; i < iFrameDuration; i += iFrameDeltaTime)
    {
        yield return new WaitForSeconds(iFrameDeltaTime);
    }

    Debug.Log("Player is no longer invincible!");
    isInvincible = false;
    }
}
