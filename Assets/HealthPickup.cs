using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20; // Iloœæ zdrowia przywracana po podniesieniu

    private void OnTriggerEnter2D(Collider2D collision) // U¿yj OnTriggerEnter, jeœli to 3D
    {
        // SprawdŸ, czy obiekt koliduj¹cy ma komponent "Health"
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Przywróæ zdrowie
            playerHealth.Heal(healAmount);

            // Zniszcz pickup
            Destroy(gameObject);
        }
    }
}
