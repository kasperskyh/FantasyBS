using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sightRange = 5f;
    [SerializeField] private float stopDuration = 0.5f; 
    Animator anim;

    private bool hasSeenPlayer = false; 
    private bool isStopped = false; 

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

            if (!hasSeenPlayer && distanceToPlayer <= sightRange)
            {
                hasSeenPlayer = true; 
            }

            if (hasSeenPlayer)
            {
                Vector3 direction = new Vector3(playerPosition.x - transform.position.x, 0, 0).normalized;
                transform.position += direction * speed * Time.deltaTime;
                anim.SetBool("isRunning", true);

                if (playerPosition.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-4, 4, 4);
                }
                else
                {
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
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(stopDuration);
        isStopped = false;
    }
}