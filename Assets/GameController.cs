using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    private Health health;

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
        health = GetComponent<Health>();  

    }

   private void OnTriggerEnter2D(Collider2D collision)
   {
    if(collision.CompareTag("Obstacle"))
    {
            health.takeDamage(5);
            Debug.Log(health.currentHealth);
    }
   }
}
