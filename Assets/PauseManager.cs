using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;   // Panel pauzy, kt�ry chcesz wy�wietli�
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false); // Panel pauzy jest pocz�tkowo niewidoczny

        // Dodajemy nas�uchiwanie na za�adowanie nowej sceny
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Sprawd�, czy u�ytkownik nacisn�� Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Funkcja do zatrzymania gry i wy�wietlenia panelu pauzy
    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Poka� panel pauzy
        Time.timeScale = 0f;        // Zatrzymaj czas gry
    }

    // Funkcja do wznowienia gry i ukrycia panelu pauzy
    void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Ukryj panel pauzy
        Time.timeScale = 1f;         // Wzn�w czas gry
    }

    // Funkcja, kt�ra �aduje scen� g��wnego menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Upewnij si�, �e czas gry jest wznowiony przed za�adowaniem menu

        SceneManager.LoadScene("MainMenu"); // Zmien "MainMenu" na nazw� swojej sceny menu
    }

    // Funkcja do rozpocz�cia od nowa gry
    public void RestartGame()
    {
        // Resetuj wynik i czas w GameManagerze
        Health health = FindObjectOfType<Health>();
        health.Respawn();
        GameManager.Instance.ResetGame();
    }

    // Nas�uchuje na zako�czenie �adowania nowej sceny
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Po za�adowaniu nowej sceny, upewnij si�, �e panel pauzy jest wy��czony
        pausePanel.SetActive(false); // Ukryj panel pauzy
        Time.timeScale = 1f; // Upewnij si�, �e czas gry jest wznawiany
    }

    // Pami�taj o usuni�ciu nas�uchiwania na zako�czenie �adowania sceny, aby unikn�� wyciek�w pami�ci
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
