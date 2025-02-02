using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private int speed = 10; // Pr�dko�� poruszania si� gracza
    [SerializeField] private float jumpForce = 10; // Si�a skoku gracza
    [SerializeField] private TrailRenderer tr; // TrailRenderer do efektu dasha
    [SerializeField] private Rigidbody2D rb; // Rigidbody2D do zarz�dzania fizyk� gracza
    private Animator anim; // Animator do zarz�dzania animacjami gracza
    private Vector3 targetScale; // Docelowa skala gracza
    public bool isGrounded; // Flaga wskazuj�ca, czy gracz jest na ziemi
    private bool doubleJump; // Flaga wskazuj�ca, czy gracz mo�e wykona� podw�jny skok
    private bool isOnWall; // Flaga wskazuj�ca, czy gracz jest na �cianie

    private bool canDash = true; // Flaga wskazuj�ca, czy gracz mo�e wykona� dash
    private bool isDashing; // Flaga wskazuj�ca, czy gracz wykonuje dash
    [SerializeField] private float dashingForce = 3f; // Si�a dasha
    private float dashingTime = 0.2f; // Czas trwania dasha
    private float dashingCooldown = 1f; // Czas odnowienia dasha

    [SerializeField] private float wallJumpForce = 0.2f; // Si�a skoku od �ciany
    [SerializeField] private Vector2 wallJumpDirection = new Vector2(1, 1); // Kierunek skoku od �ciany

    private Health health; // Referencja do skryptu Health

    // Funkcja s�u��ca do inicjalizacji komponent�w
    void Start()
    {
        anim = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        targetScale = transform.localScale; // Inicjalizacja docelowej skali
        rb = GetComponent<Rigidbody2D>(); // Inicjalizacja komponentu Rigidbody2D

        health = GetComponent<Health>(); // Inicjalizacja komponentu Health

        GameObject[] invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");

        // Zignoruj kolizj� gracza z niewidzialnymi �cianami
        foreach (GameObject wall in invisibleWalls)
        {
            Collider2D wallCollider = wall.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>(); // Zak�adaj�c, �e gracz ma Collider2D

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

    // Funkcja s�u��ca do obs�ugi zdarzenia kolizji
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

    // Funkcja s�u��ca do obs�ugi zdarzenia opuszczenia kolizji
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

    // Funkcja s�u��ca do wykonania skoku od �ciany
    private void WallJump()
    {
        Vector2 jumpDirection = new Vector2(wallJumpDirection.x * -transform.localScale.x, wallJumpDirection.y);
        rb.velocity = new Vector2(jumpDirection.x * wallJumpForce, jumpDirection.y * wallJumpForce);
        isOnWall = false;
    }

    // Funkcja s�u��ca do biegu po �cianie
    private void WallRun()
    {
        float move = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, move * speed);
    }

    // Funkcja s�u��ca do wykonania dasha
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