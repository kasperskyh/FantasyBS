using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;   // Panel pauzy, który chcesz wyœwietliæ
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false); // Panel pauzy jest pocz¹tkowo niewidoczny

        // Dodajemy nas³uchiwanie na za³adowanie nowej sceny
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // SprawdŸ, czy u¿ytkownik nacisn¹³ Escape
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

    // Funkcja do zatrzymania gry i wyœwietlenia panelu pauzy
    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Poka¿ panel pauzy
        Time.timeScale = 0f;        // Zatrzymaj czas gry
    }

    // Funkcja do wznowienia gry i ukrycia panelu pauzy
    void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Ukryj panel pauzy
        Time.timeScale = 1f;         // Wznów czas gry
    }

    // Funkcja, która ³aduje scenê g³ównego menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Upewnij siê, ¿e czas gry jest wznowiony przed za³adowaniem menu

        SceneManager.LoadScene("MainMenu"); // Zmien "MainMenu" na nazwê swojej sceny menu
    }

    // Funkcja do rozpoczêcia od nowa gry
    public void RestartGame()
    {
        // Resetuj wynik i czas w GameManagerze
        Health health = FindObjectOfType<Health>();
        health.Respawn();
        GameManager.Instance.ResetGame();
    }

    // Nas³uchuje na zakoñczenie ³adowania nowej sceny
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Po za³adowaniu nowej sceny, upewnij siê, ¿e panel pauzy jest wy³¹czony
        pausePanel.SetActive(false); // Ukryj panel pauzy
        Time.timeScale = 1f; // Upewnij siê, ¿e czas gry jest wznawiany
    }

    // Pamiêtaj o usuniêciu nas³uchiwania na zakoñczenie ³adowania sceny, aby unikn¹æ wycieków pamiêci
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
