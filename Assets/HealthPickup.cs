using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 20; // Ilo�� zdrowia przywracanego po zebraniu przedmiotu

    // Funkcja wywo�ywana przy wej�ciu w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>(); // Pobranie komponentu zdrowia gracza
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount); // Leczenie gracza

            Destroy(gameObject); // Zniszczenie obiektu przedmiotu
        }
    }
}