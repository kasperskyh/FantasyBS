using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator anim;
    private Movement movement;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SprawdŸ, czy kolizja nast¹pi³a z graczem
        if (collision.CompareTag("Player"))
        {
            // Wywo³aj animacjê "dead"
            anim.SetTrigger("dead");

            // Dodaj 50 punktów do wyniku w GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.score += 50;
                Debug.Log("Score after collision: " + GameManager.Instance.score);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {   
                    Health playerHealth = collision.gameObject.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        playerHealth.takeDamage(25);
                    }

                    Debug.Log("Gracz uderzy³ w muchomora od boku i otrzyma³ obra¿enia.");
            }
        }
    }


    public void DestroyMushroom()
    {
        Destroy(gameObject);
    }
}
