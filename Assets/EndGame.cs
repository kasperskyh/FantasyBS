using UnityEngine;
using UnityEngine.SceneManagement; // Dodaj tê liniê, aby móc u¿ywaæ SceneManager

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SprawdŸ, czy gracz wchodzi w kolizjê z obiektem o tagu "endGAME"
        if (collision.CompareTag("Player"))
        {
            // Zakoñcz grê lub za³aduj scenê koñcow¹
            EndGame();
        }
    }

    private void EndGame()
    {
        // Mo¿liwoœæ zakoñczenia gry (po zakoñczeniu gry mo¿na dodaæ ekran koñcowy lub restart)
        Debug.Log("Gra zakoñczona!"); // Mo¿esz dodaæ w³asny komunikat w konsoli
        // Jeœli chcesz zakoñczyæ grê na sta³e (np. na PC), u¿yj:
        // Application.Quit();

        // Jeœli chcesz za³adowaæ scenê koñcow¹, np. "GameOver", u¿yj:
        SceneManager.LoadScene("GameOver"); // Zamieñ "GameOverScene" na nazwê swojej sceny koñcowej

        // Jeœli chcesz zakoñczyæ grê na platformie mobilnej, u¿yj:
        // Application.Quit();
    }
}
