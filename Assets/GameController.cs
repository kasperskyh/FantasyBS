using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    private Health health;
    private bool isTakingDamage = false;

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            health.takeDamage(35);
            Debug.Log(health.currentHealth);
            if (!isTakingDamage)
            {
                StartCoroutine(TakeDamageOverTime(collision));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isTakingDamage = false;
        }
    }

    private IEnumerator TakeDamageOverTime(Collider2D collision)
    {
        isTakingDamage = true;
        while (isTakingDamage)
        {
            health.takeDamage(35);
            Debug.Log(health.currentHealth);
            yield return new WaitForSeconds(1f); // Zadawaj obra�enia co sekund�
        }
    }
}