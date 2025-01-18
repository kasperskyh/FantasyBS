using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab stworka
    public float spawnInterval = 5f;    // Czas miêdzy respawnami (5 sekund)
    public float radius = 3f;          // Promieñ, z którego stworki bêd¹ siê spawnowaæ

    private void Start()
    {
        // Rozpoczynamy korutynê, która bêdzie wywo³ywaæ spawn co okreœlony czas
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Czekamy okreœlon¹ iloœæ czasu przed respawnem
            yield return new WaitForSeconds(spawnInterval);

            // Losujemy k¹t na obwodzie ko³a (w radianach)
            float angle = Random.Range(0f, 2 * Mathf.PI);

            // Obliczamy pozycjê na obwodzie ko³a
            Vector3 spawnPosition = new Vector3(
                Mathf.Cos(angle) * radius + transform.position.x, // Pozycja X na obwodzie
                Mathf.Sin(angle) * radius + transform.position.y, // Pozycja Y na obwodzie
                transform.position.z // Pozostaje ta sama Z
            );

            // Tworzymy stworka w wylosowanej pozycji
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
