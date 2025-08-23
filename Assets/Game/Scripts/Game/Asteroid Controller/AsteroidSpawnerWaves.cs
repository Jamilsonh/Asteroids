using UnityEngine;

public class AsteroidSpawnerWaves : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform[] spawnPoints;

    public void SpawnAsteroid(WaveManager manager, WaveData waveData)
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Vector2 baseDirection = Vector2.zero;
        switch (index)
        {
            case 0: baseDirection = Vector2.down; break;
            case 1: baseDirection = Vector2.up; break;
            case 2: baseDirection = Vector2.right; break;
            case 3: baseDirection = Vector2.left; break;
        }

        float angleOffset = Random.Range(-30f, 30f);
        Vector2 finalDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPoint.position, Quaternion.identity);

        // Escala controlada pela WaveData
        float scale = Random.Range(waveData.minScale, waveData.maxScale);
        asteroid.transform.localScale = new Vector3(scale, scale, 1f);

        float speed = Random.Range(waveData.minSpeed, waveData.maxSpeed);

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        rb.linearVelocity = finalDirection.normalized * speed;

        Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
        if (asteroidScript != null)
        {
            asteroidScript.Setup(waveData);
        }

        // registra no manager
        manager.RegisterAsteroid();

        // adiciona script para avisar quando for destruído
        asteroid.AddComponent<AsteroidTrackerWave>().Setup(manager);
    }
}
