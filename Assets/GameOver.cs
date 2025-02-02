using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText; // Tekst wy�wietlaj�cy wynik gracza
    public Text timeText; // Tekst wy�wietlaj�cy czas gry
    public Button exitButton; // Przycisk do wyj�cia z gry

    // Funkcja s�u��ca do inicjalizacji komponent�w
    void Start()
    {
        if (GameManager.Instance != null)
        {
            // Ustawienie tekstu wyniku i czasu na podstawie danych z GameManager
            scoreText.text = "Score: " + GameManager.Instance.score.ToString();
            timeText.text = "Time: " + Mathf.FloorToInt(GameManager.Instance.elapsedTime).ToString() + "s";
        }
        exitButton.onClick.AddListener(ExitGame); // Dodanie nas�uchiwania na klikni�cie przycisku wyj�cia
    }

    // Funkcja s�u��ca do wyj�cia z gry
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Zatrzymanie gry w edytorze Unity
#else
        Application.Quit(); // Zako�czenie aplikacji w buildzie
#endif
    }

    // Funkcja s�u��ca do restartu gry
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Za�adowanie sceny gry od nowa
    }
}