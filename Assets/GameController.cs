using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos; // Początkowa pozycja obiektu
    private Health health; // Referencja do skryptu Health
    private bool isTakingDamage = false; // Flaga wskazująca, czy obiekt otrzymuje obrażenia

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position; // Ustawienie początkowej pozycji
        health = GetComponent<Health>(); // Inicjalizacja komponentu Health
    }

    // Funkcja wywoływana przy wejściu w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            health.takeDamage(35); // Zadanie jednorazowych obrażeń
            Debug.Log(health.currentHealth); // Wypisanie aktualnego zdrowia do konsoli
            if (!isTakingDamage)
            {
                StartCoroutine(TakeDamageOverTime(collision)); // Rozpoczęcie zadawania obrażeń w czasie
            }
        }
    }

    // Funkcja wywoływana przy wyjściu z triggera
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isTakingDamage = false; // Zatrzymanie zadawania obrażeń
        }
    }

    // Korutyna do zadawania obrażeń w czasie
    private IEnumerator TakeDamageOverTime(Collider2D collision)
    {
        isTakingDamage = true; // Ustawienie flagi zadawania obrażeń
        while (isTakingDamage)
        {
            health.takeDamage(35); // Zadanie obrażeń
            Debug.Log(health.currentHealth); // Wypisanie aktualnego zdrowia do konsoli
            yield return new WaitForSeconds(1f); // Zadawanie obrażeń co sekundę
        }
    }
}