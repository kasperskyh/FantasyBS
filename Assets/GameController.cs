using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
    }

   private void OnTriggerEnter2D(Collider2D collision)
   {
    if(collision.CompareTag("Obstacle"))
    {
        Debug.Log("Collision with obstacle");
        Respawn();
    }
   }

   private void Respawn()
   {
        transform.position = startPos;
   }
}
