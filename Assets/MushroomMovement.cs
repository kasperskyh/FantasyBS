using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public float speed = 2f; // Prêdkoœæ poruszania siê muchomora
    private int direction = 1; // Kierunek poruszania siê (1 dla prawo, -1 dla lewo)
    private Animator anim; // Animator do zarz¹dzania animacjami muchomora

    private Rigidbody2D rb; // Rigidbody2D do zarz¹dzania fizyk¹ muchomora

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicjalizacja komponentu Rigidbody2D
        anim = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
    }

    // Funkcja s³u¿¹ca do aktualizacji prêdkoœci i animacji muchomora
    private void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y); // Ustawienie prêdkoœci poruszania siê
        anim.SetBool("isWalking", true); // Ustawienie animacji chodzenia
    }

    // Funkcja s³u¿¹ca do obs³ugi zdarzenia kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            direction *= -1;  // Zmiana kierunku poruszania siê
            FlipSprite();     // Odwrócenie sprite'a
        }
    }

    // Funkcja s³u¿¹ca do odwrócenia sprite'a muchomora
    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale; // Pobranie aktualnej skali
        localScale.x *= -1; // Odwrócenie skali w osi X
        transform.localScale = localScale; // Zastosowanie nowej skali
    }
}