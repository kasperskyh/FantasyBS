using UnityEngine;

public class FlyingCreature : MonoBehaviour
{
    public float speed = 5f;           // Prêdkoœæ poruszania siê stworka
    public float damage = 10f;         // Iloœæ obra¿eñ, które zadaje stworek
    public float maxDistance = 15f;    // Maksymalna odleg³oœæ, któr¹ stworek pokona (po tej odleg³oœci stworek bêdzie kontynuowa³ lot)
    public Transform player;           // Transform gracza
    private bool isDead = false;       // Flaga sprawdzaj¹ca, czy stworek jest martwy

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
            // Zawsze œcigaj gracza
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
            // Zadaj obra¿enia graczowi
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
        // Przyjmujemy obra¿enia od ataku gracza i od razu niszczymy stworka
        DestroyCreature();

        // Dodaj 50 punktów do wyniku gracza
        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += 50;
            Debug.Log("Gracz otrzyma³ 50 punktów. Nowy wynik: " + GameManager.Instance.score);
        }

        Debug.Log("Stworek zosta³ trafiony przez gracza!");
    }

}
