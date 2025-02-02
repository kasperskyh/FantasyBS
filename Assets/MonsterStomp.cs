using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator anim; // Animator do zarz�dzania animacjami muchomora
    private Movement movement; // Referencja do skryptu Movement

    // Funkcja s�u��ca do inicjalizacji komponent�w
    void Start()
    {
        anim = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        movement = GetComponent<Movement>(); // Inicjalizacja komponentu Movement
    }

    // Funkcja s�u��ca do obs�ugi zdarzenia wej�cia w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("dead"); // Wywo�anie animacji �mierci

            if (GameManager.Instance != null)
            {
                GameManager.Instance.score += 50; // Dodanie punkt�w do wyniku
                Debug.Log("Score after collision: " + GameManager.Instance.score); // Wypisanie wyniku do konsoli
            }
        }
    }

    // Funkcja s�u��ca do obs�ugi zdarzenia kolizji
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
                    playerHealth.takeDamage(25); // Zadanie obra�e� graczowi
                }

                Debug.Log("Gracz uderzy� w muchomora od boku i otrzyma� obra�enia."); // Wypisanie komunikatu do konsoli
            }
        }
    }

    // Funkcja s�u��ca do zniszczenia obiektu muchomora
    public void DestroyMushroom()
    {
        Destroy(gameObject); // Zniszczenie obiektu muchomora
    }
}