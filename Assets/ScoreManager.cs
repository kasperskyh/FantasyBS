using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Referencja do komponentu UI, który wyœwietli wynik
    public Text timeText; // Referencja do komponentu UI, który wyœwietli czas rozgrywki

    void Update()
    {
        if (GameManager.Instance != null)
        {
            // Pobierz wynik i czas z GameManagera
            int score = GameManager.Instance.score;
            float elapsedTime = GameManager.Instance.elapsedTime;

            // Aktualizuj UI
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score.ToString();
            }

            if (timeText != null)
            {
                timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString() + "s";
            }
        }
    }
}
