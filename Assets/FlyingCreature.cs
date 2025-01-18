using UnityEngine;

public class FlyingCreature : MonoBehaviour
{
    public float speed = 5f;           // Pr�dko�� poruszania si� stworka
    public float damage = 10f;         // Ilo�� obra�e�, kt�re zadaje stworek
    public float maxDistance = 15f;    // Maksymalna odleg�o��, kt�r� stworek pokona (po tej odleg�o�ci stworek b�dzie kontynuowa� lot)
    public Transform player;           // Transform gracza
    private bool isDead = false;       // Flaga sprawdzaj�ca, czy stworek jest martwy

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (!isDead)
        {
            // Zawsze �cigaj gracza
            FlyTowardsPlayer();

        }
    }

    void FlyTowardsPlayer()
    {
        // Obliczamy kierunek do gracza
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Zadaj obra�enia graczowi
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.takeDamage(damage);
            }

            DestroyCreature();
        }
    }

    public void DestroyCreature()
    {
        isDead = true;
        Destroy(gameObject); 
    }

    public void TakeDamageFromPlayer(int damage)
    {
        // Przyjmujemy obra�enia od ataku gracza i od razu niszczymy stworka
        DestroyCreature();

        // Dodaj 50 punkt�w do wyniku gracza
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += 50;
            Debug.Log("Gracz otrzyma� 50 punkt�w. Nowy wynik: " + GameManager.Instance.score);
        }

        Debug.Log("Stworek zosta� trafiony przez gracza!");
    }

}
