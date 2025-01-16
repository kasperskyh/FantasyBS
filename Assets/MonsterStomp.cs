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
        // SprawdŸ kolizjê z boku, ale nic nie rób
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz uderzy³ w muchomora od boku.");
        }
    }

    public void DestroyMushroom()
    {
        Destroy(gameObject);
    }
}
