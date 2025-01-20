using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public int score = 10000; 
    public float elapsedTime = 0f; 

    private float timeSinceLastScoreDecrease = 0f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        timeSinceLastScoreDecrease += Time.deltaTime;

        if (timeSinceLastScoreDecrease >= 1f)
        {
            score -= 50;
            timeSinceLastScoreDecrease = 0f; 
        }

    }

    public void ResetGame()
    {
       
        score = 10000;
        elapsedTime = 0f; 
        
        timeSinceLastScoreDecrease = 0f; 
        Debug.Log("Game data has been reset!");
    }
}
