using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    Health health; // Referencja do komponentu Health gracza

    // Funkcja wywo�ywana przy starcie
    void Start()
    {
        health = GameObject.FindObjectOfType<Health>(); // Znalezienie komponentu Health w scenie
    }

    // Funkcja wywo�ywana przy wej�ciu w trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            health.Respawn(); // Wywo�anie funkcji respawnu gracza
        }
    }
}