using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText; // Tekst do wyœwietlania wyniku
    public Text timeText; // Tekst do wyœwietlania czasu
    public Button exitButton; // Przycisk do wyjœcia z gry

    void Start()
    {
        // Wyœwietl wynik i czas po za³adowaniu sceny
        if (GameManager.Instance != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score.ToString();
            timeText.text = "Time: " + Mathf.FloorToInt(GameManager.Instance.elapsedTime).ToString() + "s";
        }

        // Ustaw funkcjê wyjœcia na przycisku
        exitButton.onClick.AddListener(ExitGame);
    }

    // Funkcja, która koñczy aplikacjê
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // W przypadku pracy w edytorze Unity
#else
        Application.Quit(); // Zamyka aplikacjê w wersji zbudowanej
#endif
    }

    // Mo¿esz równie¿ dodaæ funkcjê restartu, jeœli chcesz umo¿liwiæ powrót do gry:
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Przyk³adowa scena gry
    }
}
