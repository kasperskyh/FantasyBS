using UnityEngine;
using UnityEngine.SceneManagement; // Dodaj t� lini�, aby m�c u�ywa� SceneManager

public class EndGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawd�, czy gracz wchodzi w kolizj� z obiektem o tagu "endGAME"
        if (collision.CompareTag("Player"))
        {
            // Zako�cz gr� lub za�aduj scen� ko�cow�
            EndGame();
        }
    }

    private void EndGame()
    {
        // Mo�liwo�� zako�czenia gry (po zako�czeniu gry mo�na doda� ekran ko�cowy lub restart)
        Debug.Log("Gra zako�czona!"); // Mo�esz doda� w�asny komunikat w konsoli
        // Je�li chcesz zako�czy� gr� na sta�e (np. na PC), u�yj:
        // Application.Quit();

        // Je�li chcesz za�adowa� scen� ko�cow�, np. "GameOver", u�yj:
        SceneManager.LoadScene("GameOver"); // Zamie� "GameOverScene" na nazw� swojej sceny ko�cowej

        // Je�li chcesz zako�czy� gr� na platformie mobilnej, u�yj:
        // Application.Quit();
    }
}
