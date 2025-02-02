using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 100; // Ilo�� punkt�w dodawanych po zebraniu monety

    // Funkcja wywo�ywana przy wej�ciu w trigger monety
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += scoreAmount; // Dodanie punkt�w do wyniku
            Debug.Log("Score after coin pickup: " + GameManager.Instance.score); // Wypisanie aktualnego wyniku do konsoli
        }

        Destroy(gameObject); // Zniszczenie obiektu monety
    }
}