using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int speed = 10;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private TrailRenderer tr;    
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    private Vector3 targetScale;
    private bool isGrounded;
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingForce = 3f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

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

        // P�ynna zmiana skali
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 50);

        anim.SetBool("isRunning", move != 0);

       if (isGrounded && !Input.GetButton("Jump"))
       {
            doubleJump = false;
       }

       if (Input.GetButtonDown("Jump"))
       {
        if (isGrounded || doubleJump )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            doubleJump = !doubleJump;
        }
       }

       if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
       {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
       }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        canDash =false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingForce, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}