using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject player; // Obiekt gracza
    [SerializeField] private float speed = 2f; // Prêdkoœæ poruszania siê przeciwnika
    [SerializeField] private float sightRange = 5f; // Zasiêg widzenia przeciwnika
    [SerializeField] private float stopDuration = 0.5f; // Czas zatrzymania po ataku
    Animator anim; // Animator do zarz¹dzania animacjami przeciwnika
    Health health; // Referencja do skryptu Health przeciwnika

    private bool hasSeenPlayer = false; // Flaga wskazuj¹ca, czy przeciwnik zobaczy³ gracza
    private bool isStopped = false; // Flaga wskazuj¹ca, czy przeciwnik jest zatrzymany
    private Enemy enemy; // Referencja do skryptu Enemy

    // Funkcja s³u¿¹ca do inicjalizacji komponentów
    void Start()
    {
        anim = GetComponent<Animator>(); // Inicjalizacja komponentu Animator
        enemy = GetComponent<Enemy>(); // Inicjalizacja komponentu Enemy
        health = GetComponent<Health>(); // Inicjalizacja komponentu Health
    }

    // Funkcja s³u¿¹ca do aktualizacji stanu przeciwnika w ka¿dej klatce
    void Update()
    {
        if (player != null && !isStopped && !enemy.isDead)
        {
            Vector3 playerPosition = player.transform.position;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            if (!hasSeenPlayer && distanceToPlayer <= sightRange)
            {
                hasSeenPlayer = true; // Przeciwnik zobaczy³ gracza
            }

            if (hasSeenPlayer)
            {
                Vector3 direction = new Vector3(playerPosition.x - transform.position.x, 0, 0).normalized;
                transform.position += direction * speed * Time.deltaTime; // Przemieszczanie siê przeciwnika w kierunku gracza
                anim.SetBool("isRunning", true); // Ustawienie animacji biegu

                if (playerPosition.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-4, 4, 4); // Odwrócenie przeciwnika w lewo
                }
                else
                {
                    transform.localScale = new Vector3(4, 4, 4); // Odwrócenie przeciwnika w prawo
                }
            }
        }
    }

    // Funkcja s³u¿¹ca do obs³ugi zdarzenia kolizji
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            StartCoroutine(StopMovement()); // Zatrzymanie ruchu przeciwnika i wywo³anie ataku
        }
    }

    // Funkcja s³u¿¹ca do obs³ugi zdarzenia wejœcia w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            health.takeDamage(25); // Zadanie obra¿eñ graczowi
        }
    }

    // Funkcja s³u¿¹ca do zatrzymania ruchu przeciwnika na okreœlony czas
    private IEnumerator StopMovement()
    {
        isStopped = true; // Zatrzymanie przeciwnika
        anim.SetBool("isRunning", false); // Wy³¹czenie animacji biegu
        anim.SetTrigger("Attack"); // Wywo³anie animacji ataku
        yield return new WaitForSeconds(stopDuration); // Czekanie przez okreœlony czas
        isStopped = false; // Wznowienie ruchu przeciwnika
    }
}