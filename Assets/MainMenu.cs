using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("SampleScene"); // Nazwa twojej sceny do grania
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Gra zosta³a zamkniêta."); // Debug dzia³a tylko w edytorze
    }
}
