using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    public float speed = 2f; // Pr�dko�� poruszania si� muchomora
    private int direction = 1; // Kierunek poruszania si� (1 dla prawo, -1 dla lewo)
    private Animator anim; // Animator do zarz�dzania animacjami muchomora

    private Rigidbody2D rb; // Rigidbody2D do zarz�dzania fizyk� muchomora

    // Funkcja s�u��ca do inicjalizacji komponent�w
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicjalizacja komponentu Rigidbody2D
        anim = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
    }

    // Funkcja s�u��ca do aktualizacji pr�dko�ci i animacji muchomora
    private void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y); // Ustawienie pr�dko�ci poruszania si�
        anim.SetBool("isWalking", true); // Ustawienie animacji chodzenia
    }

    // Funkcja s�u��ca do obs�ugi zdarzenia kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            direction *= -1;  // Zmiana kierunku poruszania si�
            FlipSprite();     // Odwr�cenie sprite'a
        }
    }

    // Funkcja s�u��ca do odwr�cenia sprite'a muchomora
    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale; // Pobranie aktualnej skali
        localScale.x *= -1; // Odwr�cenie skali w osi X
        transform.localScale = localScale; // Zastosowanie nowej skali
    }
}