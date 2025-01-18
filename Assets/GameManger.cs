using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton, aby uzyska� �atwy dost�p do instancji
    public int score = 10000; // Globalny wynik
    public float elapsedTime = 0f; // Globalny czas gry

    private float timeSinceLastScoreDecrease = 0f; // Czas od ostatniego odj�cia punkt�w

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Nie niszcz obiektu podczas zmiany sceny
        }
        else
        {
            Destroy(gameObject); // Usuwaj duplikaty
        }
    }

    private void Update()
    {
        // Aktualizuj czas gry
        elapsedTime += Time.deltaTime;

        // Aktualizuj czas od ostatniego odj�cia punkt�w
        timeSinceLastScoreDecrease += Time.deltaTime;

        // Je�li min�a sekunda, odejmij 100 punkt�w
        if (timeSinceLastScoreDecrease >= 1f)
        {
            score -= 100;
            timeSinceLastScoreDecrease = 0f; // Zresetuj licznik czasu dla odejmowania punkt�w
        }

        // Debugowanie w konsoli
        Debug.Log("Score: " + score + ", Time: " + Mathf.FloorToInt(elapsedTime) + "s");
    }

    // Resetowanie danych gry (np. przy wyj�ciu do menu)
    public void ResetGame()
    {
       
        score = 10000; // Resetuj wynik do pocz�tkowej warto�ci
        elapsedTime = 0f; // Resetuj czas gry
        
        timeSinceLastScoreDecrease = 0f; // Resetuj licznik czasu dla odj�cia punkt�w
        Debug.Log("Game data has been reset!");
    }
}
