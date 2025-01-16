using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public float speed = 2f; // Prêdkoœæ poruszania siê
    private int direction = 1; // Kierunek: 1 = prawo, -1 = lewo
    private Animator anim;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Poruszaj grzybka w lewo lub prawo
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        anim.SetBool("isWalking", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SprawdŸ, czy grzybek zderza siê z niewidzialn¹ œcian¹
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            direction *= -1;  // Zmieñ kierunek
            FlipSprite();     // Obróæ sprite
        }
    }

    private void FlipSprite()
    {
        // Obróæ sprite grzybka w odpowiednim kierunku
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
