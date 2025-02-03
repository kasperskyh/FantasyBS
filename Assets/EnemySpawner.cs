using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Prefab wroga, który będzie spawnowany
    public float spawnInterval = 5f;   // Interwał czasowy między spawnami wrogów
    public float radius = 3f;          // Promień wokół spawneru, w którym będą spawnowani wrogowie

    // Funkcja wywoływana przy starcie
    private void Start()
    {
        StartCoroutine(SpawnEnemy()); // Rozpoczęcie korutyny spawnującej wrogów
    }

    // Korutyna służąca do spawnowania wrogów
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Czekanie przez określony interwał czasowy

            float angle = Random.Range(0f, 2 * Mathf.PI); // Losowanie kąta w zakresie od 0 do 2π

            Vector3 spawnPosition = new Vector3(
                Mathf.Cos(angle) * radius + transform.position.x, // Obliczenie pozycji x na podstawie kąta i promienia
                Mathf.Sin(angle) * radius + transform.position.y, // Obliczenie pozycji y na podstawie kąta i promienia
                transform.position.z // Ustawienie pozycji z na wartość z spawneru
            );

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Instancjowanie wroga w obliczonej pozycji
        }
    }
}