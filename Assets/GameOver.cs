using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText; // Tekst wyœwietlaj¹cy wynik gracza
    public Text timeText; // Tekst wyœwietlaj¹cy czas gry
    public Button exitButton; // Przycisk do wyjœcia z gry

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    void Start()
    {
        if (GameManager.Instance != null)
        {
            // Ustawienie tekstu wyniku i czasu na podstawie danych z GameManager
            scoreText.text = "Score: " + GameManager.Instance.score.ToString();
            timeText.text = "Time: " + Mathf.FloorToInt(GameManager.Instance.elapsedTime).ToString() + "s";
        }
        exitButton.onClick.AddListener(ExitGame); // Dodanie nas³uchiwania na klikniêcie przycisku wyjœcia
    }

    // Funkcja s³u¿¹ca do wyjœcia z gry
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Zatrzymanie gry w edytorze Unity
#else
        Application.Quit(); // Zakoñczenie aplikacji w buildzie
#endif
    }

    // Funkcja s³u¿¹ca do restartu gry
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Za³adowanie sceny gry od nowa
    }
}