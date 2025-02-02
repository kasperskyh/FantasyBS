using UnityEngine;

public class FlyingCreature : MonoBehaviour
{
    public float speed = 5f;           // Prêdkoœæ poruszania siê stworka
    public float damage = 10f;         // Iloœæ obra¿eñ zadawanych graczowi
    public float maxDistance = 15f;    // Maksymalna odleg³oœæ, na jak¹ stworek mo¿e siê poruszaæ
    public Transform player;           // Referencja do gracza
    private bool isDead = false;       // Flaga wskazuj¹ca, czy stworek jest martwy

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Znalezienie gracza w scenie
        }
    }

    // Funkcja s³u¿¹ca do aktualizacji stanu obiektu w ka¿dej klatce
    void Update()
    {
        if (!isDead)
        {
            FlyTowardsPlayer(); // Poruszanie siê w kierunku gracza
        }
    }

    // Funkcja s³u¿¹ca do poruszania siê w kierunku gracza
    void FlyTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized; // Obliczenie kierunku do gracza
        transform.position += direction * speed * Time.deltaTime; // Poruszanie siê w kierunku gracza
    }

    // Funkcja wywo³ywana przy wejœciu w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>(); // Pobranie komponentu zdrowia gracza
            if (playerHealth != null)
            {
                playerHealth.takeDamage(damage); // Zadanie obra¿eñ graczowi
            }

            DestroyCreature(); // Zniszczenie stworka
        }
    }

    // Funkcja s³u¿¹ca do zniszczenia stworka
    public void DestroyCreature()
    {
        isDead = true; // Ustawienie flagi martwego stworka
        Destroy(gameObject); // Zniszczenie obiektu stworka
    }

    // Funkcja s³u¿¹ca do zadawania obra¿eñ stworkowi przez gracza
    public void TakeDamageFromPlayer(int damage)
    {
        DestroyCreature(); // Zniszczenie stworka

        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += 50; // Dodanie punktów do wyniku gracza
            Debug.Log("Gracz otrzyma³ 50 punktów. Nowy wynik: " + GameManager.Instance.score); // Wypisanie nowego wyniku do konsoli
        }

        Debug.Log("Stworek zosta³ trafiony przez gracza!"); // Wypisanie informacji o trafieniu stworka do konsoli
    }
}