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
        // Sprawd�, czy kolizja nast�pi�a z graczem
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("dead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawd� kolizj� z boku, ale nic nie r�b
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Gracz uderzy� w muchomora od boku.");
        }
    }

    public void DestroyMushroom()
    {
        Destroy(gameObject);
    }
}
