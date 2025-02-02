using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     
    public float spawnInterval = 5f;    
    public float radius = 3f;          

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            float angle = Random.Range(0f, 2 * Mathf.PI);

            Vector3 spawnPosition = new Vector3(
                Mathf.Cos(angle) * radius + transform.position.x, 
                Mathf.Sin(angle) * radius + transform.position.y, 
                transform.position.z 
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
