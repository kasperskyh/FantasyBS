using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Dodajemy using dla UI (Text)

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private double maxHealth = 100; // Maksymalne zdrowie
    public double currentHealth; // Aktualne zdrowie
    Animator animator; // Animator do zarz�dzania animacjami
    Vector2 startPos; // Pocz�tkowa pozycja obiektu

    [Header("iframes")]
    [SerializeField] private float iFrameDuration; // Czas trwania nie�miertelno�ci po otrzymaniu obra�e�
    [SerializeField] private float iFrameDeltaTime; // Czas mi�dzy migotaniem podczas nie�miertelno�ci
    [SerializeField] private int flashCount; // Liczba migni�� podczas nie�miertelno�ci
    private SpriteRenderer spriteRend; // Renderer sprite'a do migotania
    private bool isInvincible = false; // Flaga wskazuj�ca, czy obiekt jest nie�miertelny
    public bool isDead = false; // Flaga wskazuj�ca, czy obiekt jest martwy
    private Movement movement; // Referencja do skryptu Movement

    [Header("Dash Settings")]
    [SerializeField] private float dashInvincibilityDuration = 1f; // Czas trwania nie�miertelno�ci podczas dasha
    private bool isDashing = false; // Flaga wskazuj�ca, czy obiekt wykonuje dash

    [Header("UI Settings")]
    [SerializeField] private Text healthText; // Tekst wy�wietlaj�cy zdrowie

    // Funkcja s�u��ca do inicjalizacji komponent�w
    void Start()
    {
        startPos = transform.position; // Ustawienie pocz�tkowej pozycji
        currentHealth = maxHealth; // Ustawienie aktualnego zdrowia na maksymalne
        iFrameDuration = 2f; // Ustawienie domy�lnego czasu trwania nie�miertelno�ci
        iFrameDeltaTime = 0.2f; // Ustawienie domy�lnego czasu mi�dzy migotaniem
        animator = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        spriteRend = GetComponent<SpriteRenderer>(); // Inicjalizacja komponentu SpriteRenderer
        movement = GetComponent<Movement>(); // Inicjalizacja komponentu Movement

        UpdateHealthText(); // Aktualizacja tekstu zdrowia
    }

    // Funkcja s�u��ca do zadawania obra�e�
    public void takeDamage(double damage)
    {
        if (isInvincible) return; // Je�li obiekt jest nie�miertelny, nie zadajemy obra�e�

        currentHealth -= damage; // Zmniejszenie zdrowia

        animator.SetTrigger("Hurt"); // Wywo�anie animacji otrzymania obra�e�

        if (currentHealth <= 0)
        {
            isDead = true; // Ustawienie flagi martwego obiektu
            animator.SetBool("isDead", true); // Wywo�anie animacji �mierci
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

        StartCoroutine(Invulnerability()); // Rozpocz�cie nie�miertelno�ci
    }

    // Funkcja s�u��ca do leczenia
    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Zwi�kszenie zdrowia
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ustawienie maksymalnego zdrowia
        }
        Debug.Log("Player healed! Current health: " + currentHealth);

        UpdateHealthText(); // Aktualizacja tekstu zdrowia
    }

    // Funkcja s�u��ca do �mierci obiektu
    public void Die()
    {
        Respawn(); // Respawn obiektu
    }

    // Funkcja s�u��ca do respawnu obiektu
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
        transform.position = startPos; // Ustawienie pozycji na pocz�tkow�
        currentHealth = 100; // Ustawienie zdrowia na maksymalne
        animator.SetBool("isDead", false); // Wy��czenie animacji �mierci
        UpdateHealthText(); // Aktualizacja tekstu zdrowia

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Prze�adowanie bie��cej sceny
    }

    // Funkcja s�u��ca do aktualizacji tekstu zdrowia
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth + " / " + maxHealth; // Ustawienie tekstu zdrowia
        }
    }

    // Korutyna s�u��ca do nie�miertelno�ci po otrzymaniu obra�e�
    private IEnumerator Invulnerability()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true; // Ustawienie flagi nie�miertelno�ci

        for (int i = 0; i < flashCount; i++)
        {
            spriteRend.enabled = false; // Wy��czenie renderowania sprite'a
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

            spriteRend.enabled = true; // W��czenie renderowania sprite'a
            yield return new WaitForSeconds(iFrameDeltaTime / 2);
        }

        Debug.Log("Player is no longer invincible!");
        isInvincible = false; // Wy��czenie flagi nie�miertelno�ci
    }

    // Funkcja s�u��ca do wykonania dasha
    public void Dash()
    {
        if (isDashing) return; // Je�li obiekt ju� wykonuje dash, nie wykonujemy ponownie

        StartCoroutine(PerformDash()); // Rozpocz�cie dasha
    }

    // Korutyna s�u��ca do wykonania dasha
    private IEnumerator PerformDash()
    {
        Debug.Log("Dash started!");
        isDashing = true; // Ustawienie flagi dasha
        isInvincible = true; // Ustawienie flagi nie�miertelno�ci

        yield return new WaitForSeconds(dashInvincibilityDuration); // Czekanie na zako�czenie dasha

        Debug.Log("Dash ended, invincibility turned off!");
        isInvincible = false; // Wy��czenie flagi nie�miertelno�ci
        isDashing = false; // Wy��czenie flagi dasha
    }

    // Funkcja s�u��ca do aktualizacji stanu obiektu w ka�dej klatce
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash(); // Wykonanie dasha po naci�ni�ciu klawisza LeftShift
        }
    }
}