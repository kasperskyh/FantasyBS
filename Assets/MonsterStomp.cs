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
        // Sprawdü, czy kolizja nastπpi≥a z graczem
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("dead");
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

                    Debug.Log("Gracz uderzy≥ w muchomora od boku i otrzyma≥ obraøenia.");
            }
        }
    }


    public void DestroyMushroom()
    {
        Destroy(gameObject);
    }
}
