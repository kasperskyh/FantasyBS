using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30; // Maksymalna ilo�� zdrowia przeciwnika
    private int currentHealth; // Aktualna ilo�� zdrowia przeciwnika
    private Animator animator; // Animator do zarz�dzania animacjami przeciwnika
    private Rigidbody2D rb; // Rigidbody2D do zarz�dzania fizyk� przeciwnika
    public Boolean isDead = false; // Flaga wskazuj�ca, czy przeciwnik jest martwy

    [SerializeField] private float knockbackForce = 5f; // Si�a odrzutu po otrzymaniu obra�e�

    void Start()
    {
        animator = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        rb = GetComponent<Rigidbody2D>(); // Inicjalizacja komponentu Rigidbody2D
        currentHealth = maxHealth; // Ustawienie aktualnego zdrowia na maksymalne zdrowie
    }

    // Funkcja s�u��ca do zadawania obra�e� przeciwnikowi
    public void takeDamage(int damage, Vector2 damageSourcePosition)
    {
        currentHealth -= damage; // Zmniejszenie aktualnego zdrowia o warto�� obra�e�
        animator.SetTrigger("takeDamage"); // Wywo�anie animacji otrzymania obra�e�

        ApplyKnockback(damageSourcePosition); // Zastosowanie odrzutu

        if (currentHealth <= 0)
        {
            isDead = true; // Ustawienie flagi, �e przeciwnik jest martwy
            animator.SetBool("isDead", true); // Wywo�anie animacji �mierci
        }
    }

    // Funkcja s�u��ca do zastosowania odrzutu po otrzymaniu obra�e�
    private void ApplyKnockback(Vector2 damageSourcePosition)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - damageSourcePosition; // Obliczenie kierunku odrzutu
        knockbackDirection.Normalize(); // Normalizacja kierunku odrzutu

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // Dodanie si�y odrzutu do Rigidbody2D
    }

    // Funkcja s�u��ca do zniszczenia obiektu przeciwnika po jego �mierci
    void Die()
    {
        Destroy(gameObject); // Zniszczenie obiektu przeciwnika
        Debug.Log("Enemy died!"); // Wypisanie komunikatu do konsoli
    }
}