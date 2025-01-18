using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 100; // Liczba punkt�w przyznawana za monet�

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
