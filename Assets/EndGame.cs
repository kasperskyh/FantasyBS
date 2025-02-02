using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    // Funkcja wywo³ywana przy wejœciu w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndGame(); // Wywo³anie funkcji koñcz¹cej grê
        }
    }

    // Funkcja s³u¿¹ca do zakoñczenia gry
    private void EndGame()
    {
        Debug.Log("Gra zakoñczona!"); // Wypisanie komunikatu do konsoli

        SceneManager.LoadScene("GameOver"); // Za³adowanie sceny GameOver
    }
}