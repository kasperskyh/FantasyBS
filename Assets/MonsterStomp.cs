using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator anim; // Animator do zarz¹dzania animacjami muchomora
    private Movement movement; // Referencja do skryptu Movement

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    void Start()
    {
        anim = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        movement = GetComponent<Movement>(); // Inicjalizacja komponentu Movement
    }

    // Funkcja s³u¿¹ca do obs³ugi zdarzenia wejœcia w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("dead"); // Wywo³anie animacji œmierci

            if (GameManager.Instance != null)
            {
                GameManager.Instance.score += 50; // Dodanie punktów do wyniku
                Debug.Log("Score after collision: " + GameManager.Instance.score); // Wypisanie wyniku do konsoli
            }
        }
    }

    // Funkcja s³u¿¹ca do obs³ugi zdarzenia kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>(); // Pobranie komponentu Rigidbody2D gracza
            if (playerRb != null)
            {
                Health playerHealth = collision.gameObject.GetComponent<Health>(); // Pobranie komponentu Health gracza
                if (playerHealth != null)
                {
                    playerHealth.takeDamage(25); // Zadanie obra¿eñ graczowi
                }

                Debug.Log("Gracz uderzy³ w muchomora od boku i otrzyma³ obra¿enia."); // Wypisanie komunikatu do konsoli
            }
        }
    }

    // Funkcja s³u¿¹ca do zniszczenia obiektu muchomora
    public void DestroyMushroom()
    {
        Destroy(gameObject); // Zniszczenie obiektu muchomora
    }
}