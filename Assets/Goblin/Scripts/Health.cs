using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Dodajemy using dla UI (Text)

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
    public bool isDead = false;
    private Movement movement;

    [Header("Dash Settings")]
    [SerializeField] private float dashInvincibilityDuration = 1f;
    private bool isDashing = false;

    [Header("UI Settings")]
    [SerializeField] private Text healthText; 

    void Start()
    {
        startPos = transform.position;
        currentHealth = maxHealth;
        iFrameDuration = 2f;
        iFrameDeltaTime = 0.2f; 
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();

        UpdateHealthText();
    }

    public void takeDamage(double damage)
    {
        if (isInvincible) return; 

        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.score -= 25;
            if (GameManager.Instance.score < 0)
            {
                GameManager.Instance.score = 0; 
            }
            Debug.Log("Score after taking damage: " + GameManager.Instance.score);
        }

        UpdateHealthText();

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

        UpdateHealthText();
    }

    public void Die()
    {
        Respawn();
    }

    public void Respawn()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score -= 100;

            if (GameManager.Instance.score < 0)
            {
                GameManager.Instance.score = 0;
            }

            Debug.Log("Score after respawn: " + GameManager.Instance.score);
        }
        transform.position = startPos;
        currentHealth = 100;
        animator.SetBool("isDead", false);
        UpdateHealthText();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth + " / " + maxHealth;
        }
    }

    private IEnumerator Invulnerability()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRend.enabled = false;
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

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
        isInvincible = true; 

        yield return new WaitForSeconds(dashInvincibilityDuration);

        Debug.Log("Dash ended, invincibility turned off!");
        isInvincible = false; 
        isDashing = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }
}
