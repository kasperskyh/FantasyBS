using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false; 
    private bool hasRestarted = false;

    void Start()
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseMenu"); 
            Debug.Log("pausePanel found in Start: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PauseMenu"); 
            Debug.Log("pausePanel found in OnSceneLoaded: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); 
        }

        Time.timeScale = 1f;

        if (hasRestarted)
        {
            isPaused = false;
            hasRestarted = false; 
        }
    }

    void Update()
    {
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

    void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null)
        {
            pausePanel.SetActive(true); 
        }
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
        {
            isPaused = false;
            pausePanel.SetActive(false); 
        }
        Time.timeScale = 1f; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 

        hasRestarted = true;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); 
        GameManager.Instance.ResetGame();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
