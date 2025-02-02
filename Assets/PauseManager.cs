using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Panel pauzy
    private bool isPaused = false; // Flaga wskazuj�ca, czy gra jest wstrzymana
    private bool hasRestarted = false; // Flaga wskazuj�ca, czy gra zosta�a zrestartowana

    // Funkcja s�u��ca do inicjalizacji komponent�w
    void Start()
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseMenu"); // Znalezienie panelu pauzy w scenie
            Debug.Log("pausePanel found in Start: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Ukrycie panelu pauzy na pocz�tku
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subskrypcja zdarzenia �adowania sceny
    }

    // Funkcja wywo�ywana po za�adowaniu nowej sceny
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseMenu"); // Znalezienie panelu pauzy w nowej scenie
            Debug.Log("pausePanel found in OnSceneLoaded: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); // Ukrycie panelu pauzy po za�adowaniu sceny
        }

        Time.timeScale = 1f; // Ustawienie normalnej pr�dko�ci czasu

        if (hasRestarted)
        {
            isPaused = false; // Resetowanie flagi pauzy
            hasRestarted = false; // Resetowanie flagi restartu
        }
    }

    // Funkcja s�u��ca do aktualizacji stanu gry w ka�dej klatce
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

    // Funkcja s�u��ca do wstrzymania gry
    void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); // Wy�wietlenie panelu pauzy
        }
        Time.timeScale = 0f; // Zatrzymanie czasu w grze
    }

    // Funkcja s�u��ca do wznowienia gry
    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            isPaused = false;
            pausePanel.SetActive(false); // Ukrycie panelu pauzy
        }
        Time.timeScale = 1f; // Ustawienie normalnej pr�dko�ci czasu
    }

    // Funkcja s�u��ca do restartu gry
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ustawienie normalnej pr�dko�ci czasu

        hasRestarted = true; // Ustawienie flagi restartu

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // Za�adowanie bie��cej sceny ponownie
        GameManager.Instance.ResetGame(); // Resetowanie stanu gry
    }

    // Funkcja wywo�ywana przy zniszczeniu obiektu
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Odsubskrybowanie zdarzenia �adowania sceny
    }
}