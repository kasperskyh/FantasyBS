using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton do zarz¹dzania instancj¹ GameManager
    public int score = 10000; // Wynik gracza
    public float elapsedTime = 0f; // Czas, który up³yn¹³ od rozpoczêcia gry

    private float timeSinceLastScoreDecrease = 0f; // Czas od ostatniego zmniejszenia wyniku

    // Funkcja wywo³ywana przy tworzeniu obiektu
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Ustawienie instancji Singletona
            DontDestroyOnLoad(gameObject); // Zapobieganie zniszczeniu obiektu przy ³adowaniu nowej sceny
        }
        else
        {
            Destroy(gameObject); // Zniszczenie duplikatu GameManager
        }
    }

    // Funkcja wywo³ywana w ka¿dej klatce
    private void Update()
    {
        elapsedTime += Time.deltaTime; // Aktualizacja up³ywaj¹cego czasu

        timeSinceLastScoreDecrease += Time.deltaTime; // Aktualizacja czasu od ostatniego zmniejszenia wyniku

        if (timeSinceLastScoreDecrease >= 1f)
        {
            score -= 50; // Zmniejszenie wyniku co sekundê
            timeSinceLastScoreDecrease = 0f; // Resetowanie licznika czasu
        }
    }

    // Funkcja s³u¿¹ca do resetowania stanu gry
    public void ResetGame()
    {
        score = 10000; // Resetowanie wyniku
        elapsedTime = 0f; // Resetowanie up³ywaj¹cego czasu
        timeSinceLastScoreDecrease = 0f; // Resetowanie licznika czasu od ostatniego zmniejszenia wyniku
        Debug.Log("Game data has been reset!"); // Wypisanie informacji o resecie gry do konsoli
    }
}