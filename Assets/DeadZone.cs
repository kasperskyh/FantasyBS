using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
    Health health;
    void Start()
    {
        health = GameObject.FindObjectOfType<Health>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy obiekt, który wszedł w strefę, to gracz
        if (other.CompareTag("Player"))
        {
            // Reset poziomu (załaduj ponownie bieżącą scenę)
            health.Respawn();
        }
    }
}
