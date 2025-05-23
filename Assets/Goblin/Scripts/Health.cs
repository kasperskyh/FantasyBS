using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Dodajemy using dla UI (Text)

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private double maxHealth = 100; // Maksymalne zdrowie
    public double currentHealth; // Aktualne zdrowie
    Animator animator; // Animator do zarządzania animacjami
    Vector2 startPos; // Początkowa pozycja obiektu

    [Header("iframes")]
    [SerializeField] private float iFrameDuration; // Czas trwania nieśmiertelności po otrzymaniu obrażeń
    [SerializeField] private float iFrameDeltaTime; // Czas między migotaniem podczas nieśmiertelności
    [SerializeField] private int flashCount; // Liczba mignięć podczas nieśmiertelności
    private SpriteRenderer spriteRend; // Renderer sprite'a do migotania
    private bool isInvincible = false; // Flaga wskazująca, czy obiekt jest nieśmiertelny
    public bool isDead = false; // Flaga wskazująca, czy obiekt jest martwy
    private Movement movement; // Referencja do skryptu Movement

    [Header("Dash Settings")]
    [SerializeField] private float dashInvincibilityDuration = 1f; // Czas trwania nieśmiertelności podczas dasha
    private bool isDashing = false; // Flaga wskazująca, czy obiekt wykonuje dash

    [Header("UI Settings")]
    [SerializeField] private Text healthText; // Tekst wyświetlający zdrowie

    // Funkcja służąca do inicjalizacji komponentów
    void Start()
    {
        startPos = transform.position; // Ustawienie początkowej pozycji
        currentHealth = maxHealth; // Ustawienie aktualnego zdrowia na maksymalne
        iFrameDuration = 2f; // Ustawienie domyślnego czasu trwania nieśmiertelności
        iFrameDeltaTime = 0.2f; // Ustawienie domyślnego czasu między migotaniem
        animator = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        spriteRend = GetComponent<SpriteRenderer>(); // Inicjalizacja komponentu SpriteRenderer
        movement = GetComponent<Movement>(); // Inicjalizacja komponentu Movement

        UpdateHealthText(); // Aktualizacja tekstu zdrowia
    }

    // Funkcja służąca do zadawania obrażeń
    public void takeDamage(double damage)
    {
        if (isInvincible) return; // Jeśli obiekt jest nieśmiertelny, nie zadajemy obrażeń

        currentHealth -= damage; // Zmniejszenie zdrowia

        animator.SetTrigger("Hurt"); // Wywołanie animacji otrzymania obrażeń

        if (currentHealth <= 0)
        {
            isDead = true; // Ustawienie flagi martwego obiektu
            animator.SetBool("isDead", true); // Wywołanie animacji śmierci
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.score -= 25; // Zmniejszenie wyniku
            if (GameManager.Instance.score < 0)
            {
                GameManager.Instance.score = 0; // Ustawienie minimalnego wyniku na 0
            }
            Debug.Log("Score after taking damage: " + GameManager.Instance.score);
        }

        if (isDead)
        {
            Respawn(); // Respawn obiektu
        }

        UpdateHealthText(); // Aktualizacja tekstu zdrowia

        StartCoroutine(Invulnerability()); // Rozpoczęcie nieśmiertelności
    }

    // Funkcja służąca do leczenia
    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Zwiększenie zdrowia
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ustawienie maksymalnego zdrowia
        }
        Debug.Log("Player healed! Current health: " + currentHealth);

        UpdateHealthText(); // Aktualizacja tekstu zdrowia
    }

    // Funkcja służąca do śmierci obiektu
    public void Die()
    {
        Respawn(); // Respawn obiektu
    }

    // Funkcja służąca do respawnu obiektu
    public void Respawn()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score -= 100; // Zmniejszenie wyniku

            if (GameManager.Instance.score < 0)
            {
                GameManager.Instance.score = 0; // Ustawienie minimalnego wyniku na 0
            }

            Debug.Log("Score after respawn: " + GameManager.Instance.score);
        }
        transform.position = startPos; // Ustawienie pozycji na początkową
        currentHealth = 100; // Ustawienie zdrowia na maksymalne
        animator.SetBool("isDead", false); // Wyłączenie animacji śmierci
        UpdateHealthText(); // Aktualizacja tekstu zdrowia

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Przeładowanie bieżącej sceny
    }

    // Funkcja służąca do aktualizacji tekstu zdrowia
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth + " / " + maxHealth; // Ustawienie tekstu zdrowia
        }
    }

    // Korutyna służąca do nieśmiertelności po otrzymaniu obrażeń
    private IEnumerator Invulnerability()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true; // Ustawienie flagi nieśmiertelności

        for (int i = 0; i < flashCount; i++)
        {
            spriteRend.enabled = false; // Wyłączenie renderowania sprite'a
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

            spriteRend.enabled = true; // Włączenie renderowania sprite'a
            yield return new WaitForSeconds(iFrameDeltaTime / 2);
        }

        Debug.Log("Player is no longer invincible!");
        isInvincible = false; // Wyłączenie flagi nieśmiertelności
    }

    // Funkcja służąca do wykonania dasha
    public void Dash()
    {
        if (isDashing) return; // Jeśli obiekt już wykonuje dash, nie wykonujemy ponownie

        StartCoroutine(PerformDash()); // Rozpoczęcie dasha
    }

    // Korutyna służąca do wykonania dasha
    private IEnumerator PerformDash()
    {
        Debug.Log("Dash started!");
        isDashing = true; // Ustawienie flagi dasha
        isInvincible = true; // Ustawienie flagi nieśmiertelności

        yield return new WaitForSeconds(dashInvincibilityDuration); // Czekanie na zakończenie dasha

        Debug.Log("Dash ended, invincibility turned off!");
        isInvincible = false; // Wyłączenie flagi nieśmiertelności
        isDashing = false; // Wyłączenie flagi dasha
    }

    // Funkcja służąca do aktualizacji stanu obiektu w każdej klatce
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash(); // Wykonanie dasha po naciśnięciu klawisza LeftShift
        }
    }
}