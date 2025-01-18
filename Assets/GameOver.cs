using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text scoreText; // Tekst do wy�wietlania wyniku
    public Text timeText; // Tekst do wy�wietlania czasu
    public Button exitButton; // Przycisk do wyj�cia z gry

    void Start()
    {
        // Wy�wietl wynik i czas po za�adowaniu sceny
        if (GameManager.Instance != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score.ToString();
            timeText.text = "Time: " + Mathf.FloorToInt(GameManager.Instance.elapsedTime).ToString() + "s";
        }

        // Ustaw funkcj� wyj�cia na przycisku
        exitButton.onClick.AddListener(ExitGame);
    }

    // Funkcja, kt�ra ko�czy aplikacj�
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // W przypadku pracy w edytorze Unity
#else
        Application.Quit(); // Zamyka aplikacj� w wersji zbudowanej
#endif
    }

    // Mo�esz r�wnie� doda� funkcj� restartu, je�li chcesz umo�liwi� powr�t do gry:
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // Przyk�adowa scena gry
    }
}
