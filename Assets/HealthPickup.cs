using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20; // Ilo�� zdrowia przywracana po podniesieniu

    private void OnTriggerEnter2D(Collider2D collision) // U�yj OnTriggerEnter, je�li to 3D
    {
        // Sprawd�, czy obiekt koliduj�cy ma komponent "Health"
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            // Przywr�� zdrowie
            playerHealth.Heal(healAmount);

            // Zniszcz pickup
            Destroy(gameObject);
        }
    }
}
