using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private float dashInvincibilityDuration = 1f; // Czas nieśmiertelności po dashu
    private bool isDashing = false;

    [Header("UI Settings")]
    [SerializeField] private Text healthText; // Dodaj referencję do UI Text, który będzie wyświetlał zdrowie

    void Start()
    {
        startPos = transform.position;
        currentHealth = maxHealth;
        iFrameDuration = 2f;
        iFrameDeltaTime = 0.2f; // Czas przerwy między mignięciami
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();

        // Zaktualizuj tekst na początku gry
        UpdateHealthText();
    }

    public void takeDamage(double damage)
    {
        if (isInvincible) return; // Zignoruj obrażenia, jeśli postać jest nieśmiertelna
        if (!movement.isGrounded) return; // Zignoruj obrażenia, jeśli postać nie dotyka ziemi

        // Zmniejsz zdrowie
        currentHealth -= damage;

        // Dodaj animację "Hurt"
        animator.SetTrigger("Hurt");

        // Sprawdź, czy zdrowie spadło do zera
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetBool("isDead", true);
        }

        // Odejmij 25 punktów od wyniku za otrzymanie obrażeń
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score -= 25;
            if (GameManager.Instance.score < 0)
            {
                GameManager.Instance.score = 0; // Zapewnia, że wynik nie spadnie poniżej 0
            }
            Debug.Log("Score after taking damage: " + GameManager.Instance.score);
        }

        // Zaktualizuj tekst po otrzymaniu obrażeń
        UpdateHealthText();

        // Uruchom korutynę dla nieśmiertelności
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

        // Zaktualizuj tekst po leczeniu
        UpdateHealthText();
    }

    public void Die()
    {
        Respawn();
    }

    public void Respawn()
    {
        // Odejmij punkty od wyniku w GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score -= 100;

            // Upewnij się, że wynik nie spada poniżej zera
            if (GameManager.Instance.score < 0)
            {
                GameManager.Instance.score = 0;
            }

            Debug.Log("Score after respawn: " + GameManager.Instance.score);
        }

        // Resetuj pozycję gracza
        transform.position = startPos;

        // Przywróć zdrowie gracza
        currentHealth = 100;

        // Ustaw animatora, aby przestał pokazywać śmierć
        animator.SetBool("isDead", false);

        // Zaktualizuj tekst po respawnie
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        // Sprawdź, czy healthText jest przypisany, i zaktualizuj tekst
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
            // Wyłącz sprite, aby postać zniknęła
            spriteRend.enabled = false;
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

            // Włącz sprite, aby postać się pojawiła
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
        isInvincible = true; // Włącz nieśmiertelność na czas dasha

        // Symulacja czasu trwania dasha (1 sekunda)
        yield return new WaitForSeconds(dashInvincibilityDuration);

        Debug.Log("Dash ended, invincibility turned off!");
        isInvincible = false; // Wyłącz nieśmiertelność po dashu
        isDashing = false;
    }

    private void Update()
    {
        // Obsługa wciśnięcia klawisza dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }
}
