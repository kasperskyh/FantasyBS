using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public float speed = 2f; // Pr�dko�� poruszania si�
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
        // Sprawd�, czy grzybek zderza si� z niewidzialn� �cian�
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            direction *= -1;  // Zmie� kierunek
            FlipSprite();     // Obr�� sprite
        }
    }

    private void FlipSprite()
    {
        // Obr�� sprite grzybka w odpowiednim kierunku
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
