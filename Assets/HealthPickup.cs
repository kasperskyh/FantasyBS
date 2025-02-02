using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20; 

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);

            Destroy(gameObject);
        }
    }
}
