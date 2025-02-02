using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30; // Maksymalna iloœæ zdrowia przeciwnika
    private int currentHealth; // Aktualna iloœæ zdrowia przeciwnika
    private Animator animator; // Animator do zarz¹dzania animacjami przeciwnika
    private Rigidbody2D rb; // Rigidbody2D do zarz¹dzania fizyk¹ przeciwnika
    public Boolean isDead = false; // Flaga wskazuj¹ca, czy przeciwnik jest martwy

    [SerializeField] private float knockbackForce = 5f; // Si³a odrzutu po otrzymaniu obra¿eñ

    void Start()
    {
        animator = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        rb = GetComponent<Rigidbody2D>(); // Inicjalizacja komponentu Rigidbody2D
        currentHealth = maxHealth; // Ustawienie aktualnego zdrowia na maksymalne zdrowie
    }

    // Funkcja s³u¿¹ca do zadawania obra¿eñ przeciwnikowi
    public void takeDamage(int damage, Vector2 damageSourcePosition)
    {
        currentHealth -= damage; // Zmniejszenie aktualnego zdrowia o wartoœæ obra¿eñ
        animator.SetTrigger("takeDamage"); // Wywo³anie animacji otrzymania obra¿eñ

        ApplyKnockback(damageSourcePosition); // Zastosowanie odrzutu

        if (currentHealth <= 0)
        {
            isDead = true; // Ustawienie flagi, ¿e przeciwnik jest martwy
            animator.SetBool("isDead", true); // Wywo³anie animacji œmierci
        }
    }

    // Funkcja s³u¿¹ca do zastosowania odrzutu po otrzymaniu obra¿eñ
    private void ApplyKnockback(Vector2 damageSourcePosition)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - damageSourcePosition; // Obliczenie kierunku odrzutu
        knockbackDirection.Normalize(); // Normalizacja kierunku odrzutu

        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // Dodanie si³y odrzutu do Rigidbody2D
    }

    // Funkcja s³u¿¹ca do zniszczenia obiektu przeciwnika po jego œmierci
    void Die()
    {
        Destroy(gameObject); // Zniszczenie obiektu przeciwnika
        Debug.Log("Enemy died!"); // Wypisanie komunikatu do konsoli
    }
}