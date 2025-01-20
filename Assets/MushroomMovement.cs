using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public float speed = 2f; 
    private int direction = 1; 
    private Animator anim;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        anim.SetBool("isWalking", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // SprawdŸ, czy grzybek zderza siê z niewidzialn¹ œcian¹
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            direction *= -1;  
            FlipSprite();     
        }
    }

    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
