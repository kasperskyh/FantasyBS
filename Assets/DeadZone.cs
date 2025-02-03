using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    Health health; // Referencja do komponentu Health gracza

    // Funkcja wywo³ywana przy starcie
    void Start()
    {
        health = GameObject.FindObjectOfType<Health>(); // Znalezienie komponentu Health w scenie
    }

    // Funkcja wywo³ywana przy wejœciu w trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            health.Respawn(); // Wywo³anie funkcji respawnu gracza
        }
    }
}