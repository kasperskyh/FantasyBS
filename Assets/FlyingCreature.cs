using UnityEngine;

public class FlyingCreature : MonoBehaviour
{
    public float speed = 5f;           
    public float damage = 10f;         
    public float maxDistance = 15f;    
    public Transform player;           
    private bool isDead = false;       

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
            FlyTowardsPlayer();

        }
    }

    void FlyTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
        DestroyCreature();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.score += 50;
            Debug.Log("Gracz otrzyma³ 50 punktów. Nowy wynik: " + GameManager.Instance.score);
        }

        Debug.Log("Stworek zosta³ trafiony przez gracza!");
    }

}
