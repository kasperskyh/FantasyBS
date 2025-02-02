using UnityEngine;

public class FlyingCreature : MonoBehaviour
{
    public float speed = 5f;           // Pr�dko�� poruszania si� stworka
    public float damage = 10f;         // Ilo�� obra�e� zadawanych graczowi
    public float maxDistance = 15f;    // Maksymalna odleg�o��, na jak� stworek mo�e si� porusza�
    public Transform player;           // Referencja do gracza
    private bool isDead = false;       // Flaga wskazuj�ca, czy stworek jest martwy

    // Funkcja s�u��ca do inicjalizacji komponent�w
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Znalezienie gracza w scenie
        }
    }

    // Funkcja s�u��ca do aktualizacji stanu obiektu w ka�dej klatce
    void Update()
    {
        if (!isDead)
        {
            FlyTowardsPlayer(); // Poruszanie si� w kierunku gracza
        }
    }

    // Funkcja s�u��ca do poruszania si� w kierunku gracza
    void FlyTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized; // Obliczenie kierunku do gracza
        transform.position += direction * speed * Time.deltaTime; // Poruszanie si� w kierunku gracza
    }

    // Funkcja wywo�ywana przy wej�ciu w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>(); // Pobranie komponentu zdrowia gracza
            if (playerHealth != null)
            {
                playerHealth.takeDamage(damage); // Zadanie obra�e� graczowi
            }

            DestroyCreature(); // Zniszczenie stworka
        }
    }

    // Funkcja s�u��ca do zniszczenia stworka
    public void DestroyCreature()
    {
        isDead = true; // Ustawienie flagi martwego stworka
        Destroy(gameObject); // Zniszczenie obiektu stworka
    }

    // Funkcja s�u��ca do zadawania obra�e� stworkowi przez gracza
    public void TakeDamageFromPlayer(int damage)
    {
        DestroyCreature(); // Zniszczenie stworka

        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += 50; // Dodanie punkt�w do wyniku gracza
            Debug.Log("Gracz otrzyma� 50 punkt�w. Nowy wynik: " + GameManager.Instance.score); // Wypisanie nowego wyniku do konsoli
        }

        Debug.Log("Stworek zosta� trafiony przez gracza!"); // Wypisanie informacji o trafieniu stworka do konsoli
    }
}