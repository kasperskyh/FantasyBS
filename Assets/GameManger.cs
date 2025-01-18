using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton, aby uzyskaæ ³atwy dostêp do instancji
    public int score = 10000; // Globalny wynik
    public float elapsedTime = 0f; // Globalny czas gry

    private float timeSinceLastScoreDecrease = 0f; // Czas od ostatniego odjêcia punktów

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

        // Aktualizuj czas od ostatniego odjêcia punktów
        timeSinceLastScoreDecrease += Time.deltaTime;

        // Jeœli minê³a sekunda, odejmij 100 punktów
        if (timeSinceLastScoreDecrease >= 1f)
        {
            score -= 100;
            timeSinceLastScoreDecrease = 0f; // Zresetuj licznik czasu dla odejmowania punktów
        }

        // Debugowanie w konsoli
        Debug.Log("Score: " + score + ", Time: " + Mathf.FloorToInt(elapsedTime) + "s");
    }

    // Resetowanie danych gry (np. przy wyjœciu do menu)
    public void ResetGame()
    {
       
        score = 10000; // Resetuj wynik do pocz¹tkowej wartoœci
        elapsedTime = 0f; // Resetuj czas gry
        
        timeSinceLastScoreDecrease = 0f; // Resetuj licznik czasu dla odjêcia punktów
        Debug.Log("Game data has been reset!");
    }
}
