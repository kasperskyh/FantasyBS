using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Dodajemy using dla UI (Text)

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private double maxHealth = 100; // Maksymalne zdrowie
    public double currentHealth; // Aktualne zdrowie
    Animator animator; // Animator do zarz¹dzania animacjami
    Vector2 startPos; // Pocz¹tkowa pozycja obiektu

    [Header("iframes")]
    [SerializeField] private float iFrameDuration; // Czas trwania nieœmiertelnoœci po otrzymaniu obra¿eñ
    [SerializeField] private float iFrameDeltaTime; // Czas miêdzy migotaniem podczas nieœmiertelnoœci
    [SerializeField] private int flashCount; // Liczba migniêæ podczas nieœmiertelnoœci
    private SpriteRenderer spriteRend; // Renderer sprite'a do migotania
    private bool isInvincible = false; // Flaga wskazuj¹ca, czy obiekt jest nieœmiertelny
    public bool isDead = false; // Flaga wskazuj¹ca, czy obiekt jest martwy
    private Movement movement; // Referencja do skryptu Movement

    [Header("Dash Settings")]
    [SerializeField] private float dashInvincibilityDuration = 1f; // Czas trwania nieœmiertelnoœci podczas dasha
    private bool isDashing = false; // Flaga wskazuj¹ca, czy obiekt wykonuje dash

    [Header("UI Settings")]
    [SerializeField] private Text healthText; // Tekst wyœwietlaj¹cy zdrowie

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    void Start()
    {
        startPos = transform.position; // Ustawienie pocz¹tkowej pozycji
        currentHealth = maxHealth; // Ustawienie aktualnego zdrowia na maksymalne
        iFrameDuration = 2f; // Ustawienie domyœlnego czasu trwania nieœmiertelnoœci
        iFrameDeltaTime = 0.2f; // Ustawienie domyœlnego czasu miêdzy migotaniem
        animator = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        spriteRend = GetComponent<SpriteRenderer>(); // Inicjalizacja komponentu SpriteRenderer
        movement = GetComponent<Movement>(); // Inicjalizacja komponentu Movement

        UpdateHealthText(); // Aktualizacja tekstu zdrowia
    }

    // Funkcja s³u¿¹ca do zadawania obra¿eñ
    public void takeDamage(double damage)
    {
        if (isInvincible) return; // Jeœli obiekt jest nieœmiertelny, nie zadajemy obra¿eñ

        currentHealth -= damage; // Zmniejszenie zdrowia

        animator.SetTrigger("Hurt"); // Wywo³anie animacji otrzymania obra¿eñ

        if (currentHealth <= 0)
        {
            isDead = true; // Ustawienie flagi martwego obiektu
            animator.SetBool("isDead", true); // Wywo³anie animacji œmierci
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

        StartCoroutine(Invulnerability()); // Rozpoczêcie nieœmiertelnoœci
    }

    // Funkcja s³u¿¹ca do leczenia
    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Zwiêkszenie zdrowia
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ustawienie maksymalnego zdrowia
        }
        Debug.Log("Player healed! Current health: " + currentHealth);

        UpdateHealthText(); // Aktualizacja tekstu zdrowia
    }

    // Funkcja s³u¿¹ca do œmierci obiektu
    public void Die()
    {
        Respawn(); // Respawn obiektu
    }

    // Funkcja s³u¿¹ca do respawnu obiektu
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
        transform.position = startPos; // Ustawienie pozycji na pocz¹tkow¹
        currentHealth = 100; // Ustawienie zdrowia na maksymalne
        animator.SetBool("isDead", false); // Wy³¹czenie animacji œmierci
        UpdateHealthText(); // Aktualizacja tekstu zdrowia

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Prze³adowanie bie¿¹cej sceny
    }

    // Funkcja s³u¿¹ca do aktualizacji tekstu zdrowia
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth + " / " + maxHealth; // Ustawienie tekstu zdrowia
        }
    }

    // Korutyna s³u¿¹ca do nieœmiertelnoœci po otrzymaniu obra¿eñ
    private IEnumerator Invulnerability()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true; // Ustawienie flagi nieœmiertelnoœci

        for (int i = 0; i < flashCount; i++)
        {
            spriteRend.enabled = false; // Wy³¹czenie renderowania sprite'a
            yield return new WaitForSeconds(iFrameDeltaTime / 2);

            spriteRend.enabled = true; // W³¹czenie renderowania sprite'a
            yield return new WaitForSeconds(iFrameDeltaTime / 2);
        }

        Debug.Log("Player is no longer invincible!");
        isInvincible = false; // Wy³¹czenie flagi nieœmiertelnoœci
    }

    // Funkcja s³u¿¹ca do wykonania dasha
    public void Dash()
    {
        if (isDashing) return; // Jeœli obiekt ju¿ wykonuje dash, nie wykonujemy ponownie

        StartCoroutine(PerformDash()); // Rozpoczêcie dasha
    }

    // Korutyna s³u¿¹ca do wykonania dasha
    private IEnumerator PerformDash()
    {
        Debug.Log("Dash started!");
        isDashing = true; // Ustawienie flagi dasha
        isInvincible = true; // Ustawienie flagi nieœmiertelnoœci

        yield return new WaitForSeconds(dashInvincibilityDuration); // Czekanie na zakoñczenie dasha

        Debug.Log("Dash ended, invincibility turned off!");
        isInvincible = false; // Wy³¹czenie flagi nieœmiertelnoœci
        isDashing = false; // Wy³¹czenie flagi dasha
    }

    // Funkcja s³u¿¹ca do aktualizacji stanu obiektu w ka¿dej klatce
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash(); // Wykonanie dasha po naciœniêciu klawisza LeftShift
        }
    }
}