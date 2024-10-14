using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int speed = 10;
    [SerializeField] private float jumpForce = 10;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 targetScale;
    private bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetScale = transform.localScale; // Inicjalizacja docelowej skali
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        transform.position += new Vector3(move, 0, 0) * Time.deltaTime * speed;

        if (move > 0)
        {
            targetScale = new Vector3(4, 4, 4);
        }
        else if (move < 0)
        {
            targetScale = new Vector3(-4, 4, 4);
        }

        // P³ynna zmiana skali
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 50);

        anim.SetBool("isRunning", move != 0);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}