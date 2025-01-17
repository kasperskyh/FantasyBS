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
    public bool isGrounded;
    private bool doubleJump;
    private bool isOnWall;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingForce = 3f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private float wallJumpForce = 0.2f;
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 1);

    private Health health;

    void Start()
    {
        anim = GetComponent<Animator>();
        targetScale = transform.localScale; // Inicjalizacja docelowej skali
        rb = GetComponent<Rigidbody2D>();

        health = GetComponent<Health>();

        GameObject[] invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");

        // Zignoruj kolizjê gracza z niewidzialnymi œcianami
        foreach (GameObject wall in invisibleWalls)
        {
            Collider2D wallCollider = wall.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>(); // Zak³adaj¹c, ¿e gracz ma Collider2D

            Physics2D.IgnoreCollision(playerCollider, wallCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!health.isDead)
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

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 50);

            anim.SetBool("isRunning", move != 0);
            anim.SetBool("isJumping", !isGrounded);

            if (isGrounded && !Input.GetButton("Jump"))
            {
                doubleJump = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded || doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    doubleJump = !doubleJump;
                }
                else if (isOnWall)
                {
                    WallJump();
                }
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && canDash)
            {
                StartCoroutine(Dash());
                anim.SetTrigger("Dash");
            }

            if (isOnWall && !isGrounded)
            {
                WallRun();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", true);
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = false;
        }
    }

    private void WallJump()
    {
        Vector2 jumpDirection = new Vector2(wallJumpDirection.x * -transform.localScale.x, wallJumpDirection.y);
        rb.velocity = new Vector2(jumpDirection.x * wallJumpForce, jumpDirection.y * wallJumpForce);
        isOnWall = false;
    }

    private void WallRun()
    {
        float move = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, move * speed);
    }

    private IEnumerator Dash()
    {
        canDash = false;
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