using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton do zarz�dzania instancj� GameManager
    public int score = 10000; // Wynik gracza
    public float elapsedTime = 0f; // Czas, kt�ry up�yn�� od rozpocz�cia gry

    private float timeSinceLastScoreDecrease = 0f; // Czas od ostatniego zmniejszenia wyniku

    // Funkcja wywo�ywana przy tworzeniu obiektu
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Ustawienie instancji Singletona
            DontDestroyOnLoad(gameObject); // Zapobieganie zniszczeniu obiektu przy �adowaniu nowej sceny
        }
        else
        {
            Destroy(gameObject); // Zniszczenie duplikatu GameManager
        }
    }

    // Funkcja wywo�ywana w ka�dej klatce
    private void Update()
    {
        elapsedTime += Time.deltaTime; // Aktualizacja up�ywaj�cego czasu

        timeSinceLastScoreDecrease += Time.deltaTime; // Aktualizacja czasu od ostatniego zmniejszenia wyniku

        if (timeSinceLastScoreDecrease >= 1f)
        {
            score -= 50; // Zmniejszenie wyniku co sekund�
            timeSinceLastScoreDecrease = 0f; // Resetowanie licznika czasu
        }
    }

    // Funkcja s�u��ca do resetowania stanu gry
    public void ResetGame()
    {
        score = 10000; // Resetowanie wyniku
        elapsedTime = 0f; // Resetowanie up�ywaj�cego czasu
        timeSinceLastScoreDecrease = 0f; // Resetowanie licznika czasu od ostatniego zmniejszenia wyniku
        Debug.Log("Game data has been reset!"); // Wypisanie informacji o resecie gry do konsoli
    }
}