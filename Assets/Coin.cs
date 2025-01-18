using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 100; // Liczba punktów przyznawana za monetê

    private void OnTriggerEnter2D(Collider2D collision)
    {
           if (GameManager.Instance != null)
           {
                GameManager.Instance.score += scoreAmount;
                Debug.Log("Score after coin pickup: " + GameManager.Instance.score);
           }

           Destroy(gameObject);
    }
}
