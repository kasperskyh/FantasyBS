using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab stworka
    public float spawnInterval = 5f;    // Czas mi�dzy respawnami (5 sekund)
    public float radius = 3f;          // Promie�, z kt�rego stworki b�d� si� spawnowa�

    private void Start()
    {
        // Rozpoczynamy korutyn�, kt�ra b�dzie wywo�ywa� spawn co okre�lony czas
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Czekamy okre�lon� ilo�� czasu przed respawnem
            yield return new WaitForSeconds(spawnInterval);

            // Losujemy k�t na obwodzie ko�a (w radianach)
            float angle = Random.Range(0f, 2 * Mathf.PI);

            // Obliczamy pozycj� na obwodzie ko�a
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
