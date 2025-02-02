using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    // Funkcja wywo�ywana przy wej�ciu w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EndGame(); // Wywo�anie funkcji ko�cz�cej gr�
        }
    }

    // Funkcja s�u��ca do zako�czenia gry
    private void EndGame()
    {
        Debug.Log("Gra zako�czona!"); // Wypisanie komunikatu do konsoli

        SceneManager.LoadScene("GameOver"); // Za�adowanie sceny GameOver
    }
}