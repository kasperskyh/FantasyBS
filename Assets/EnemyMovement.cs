using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sightRange = 5f;
    [SerializeField] private float stopDuration = 1f; // Czas zatrzymania po zderzeniu
    Animator anim;

    private bool hasSeenPlayer = false; // Flaga œledz¹ca, czy wróg zobaczy³ gracza
    private bool isStopped = false; // Flaga œledz¹ca, czy wróg jest zatrzymany

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && !isStopped)
        {
            Vector3 playerPosition = player.transform.position;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            // SprawdŸ, czy gracz jest w zasiêgu widzenia, jeœli wróg jeszcze go nie zobaczy³
            if (!hasSeenPlayer && distanceToPlayer <= sightRange)
            {
                hasSeenPlayer = true; // Ustaw flagê, gdy wróg zobaczy gracza
            }

            // Jeœli wróg zobaczy³ gracza, pod¹¿aj za nim
            if (hasSeenPlayer)
            {
                Vector3 direction = new Vector3(playerPosition.x - transform.position.x, 0, 0).normalized;
                transform.position += direction * speed * Time.deltaTime;
                anim.SetBool("isRunning", true);

                // Obróæ wroga w kierunku gracza
                if (playerPosition.x < transform.position.x)
                {
                    // Gracz jest po lewej stronie, obróæ w lewo
                    transform.localScale = new Vector3(-4, 4, 4);
                }
                else
                {
                    // Gracz jest po prawej stronie, obróæ w prawo
                    transform.localScale = new Vector3(4, 4, 4);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(StopMovement());
        }
    }

    private IEnumerator StopMovement()
    {
        isStopped = true;
        anim.SetBool("isRunning", false);
        yield return new WaitForSeconds(stopDuration);
        isStopped = false;
    }
}