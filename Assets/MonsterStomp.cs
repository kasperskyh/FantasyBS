using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SprawdŸ, czy kolizja nast¹pi³a z graczem
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("dead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SprawdŸ kolizjê z graczem
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // SprawdŸ, czy gracz nie jest w locie (prêdkoœæ Y ~= 0)
                if (Mathf.Abs(playerRb.velocity.y) < 0.1f) // Tolerancja 0.1f
                {
                    // Pobierz komponent "Health" gracza i u¿yj funkcji "takeDamage"
                    Health playerHealth = collision.gameObject.GetComponent<Health>();
                    if (playerHealth != null)
                    {
                        playerHealth.takeDamage(10);
                    }

                    Debug.Log("Gracz uderzy³ w muchomora od boku i otrzyma³ obra¿enia.");
                }
                else
                {
                    Debug.Log("Gracz jest w locie i nie otrzymuje obra¿eñ.");
                }
            }
        }
    }


    public void DestroyMushroom()
    {
        Destroy(gameObject);
    }
}
