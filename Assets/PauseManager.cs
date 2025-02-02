using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Panel pauzy
    private bool isPaused = false; // Flaga wskazuj¹ca, czy gra jest wstrzymana
    private bool hasRestarted = false; // Flaga wskazuj¹ca, czy gra zosta³a zrestartowana

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    void Start()
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseMenu"); // Znalezienie panelu pauzy w scenie
            Debug.Log("pausePanel found in Start: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Ukrycie panelu pauzy na pocz¹tku
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subskrypcja zdarzenia ³adowania sceny
    }

    // Funkcja wywo³ywana po za³adowaniu nowej sceny
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseMenu"); // Znalezienie panelu pauzy w nowej scenie
            Debug.Log("pausePanel found in OnSceneLoaded: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Ukrycie panelu pauzy po za³adowaniu sceny
        }

        Time.timeScale = 1f; // Ustawienie normalnej prêdkoœci czasu

        if (hasRestarted)
        {
            isPaused = false; // Resetowanie flagi pauzy
            hasRestarted = false; // Resetowanie flagi restartu
        }
    }

    // Funkcja s³u¿¹ca do aktualizacji stanu gry w ka¿dej klatce
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Wznowienie gry
            }
            else
            {
                PauseGame(); // Wstrzymanie gry
            }
        }
    }

    // Funkcja s³u¿¹ca do wstrzymania gry
    void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); // Wyœwietlenie panelu pauzy
        }
        Time.timeScale = 0f; // Zatrzymanie czasu w grze
    }

    // Funkcja s³u¿¹ca do wznowienia gry
    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            isPaused = false;
            pausePanel.SetActive(false); // Ukrycie panelu pauzy
        }
        Time.timeScale = 1f; // Ustawienie normalnej prêdkoœci czasu
    }

    // Funkcja s³u¿¹ca do restartu gry
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ustawienie normalnej prêdkoœci czasu

        hasRestarted = true; // Ustawienie flagi restartu

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // Za³adowanie bie¿¹cej sceny ponownie
        GameManager.Instance.ResetGame(); // Resetowanie stanu gry
    }

    // Funkcja wywo³ywana przy zniszczeniu obiektu
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Odsubskrybowanie zdarzenia ³adowania sceny
    }
}