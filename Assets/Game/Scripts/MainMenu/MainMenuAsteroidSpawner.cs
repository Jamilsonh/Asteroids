using UnityEngine;

public class MainMenuAsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float spawnDelay = 3f;
    public int maxAsteroids = 2;
    public float asteroidSpeed = 3f;
    public float angleVariation = 30f;

    private float timer = 0f;
    private int currentAsteroids = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay && currentAsteroids < maxAsteroids)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        // Escolhe aleatoriamente esquerda ou direita
        bool spawnRight = Random.Range(0, 2) == 0;
        Transform spawnPoint = spawnRight ? rightSpawnPoint : leftSpawnPoint;

        // Instancia o asteroide
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPoint.position, Quaternion.identity);

        // Direção base (oposta ao ponto de spawn)
        Vector3 direction = spawnRight ? Vector3.left : Vector3.right;

        // Adiciona variação de ângulo
        float randomAngle = Random.Range(-angleVariation, angleVariation);
        direction = Quaternion.Euler(0, 0, randomAngle) * direction;

        // Aplica velocidade
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * asteroidSpeed;

        // Atualiza contagem de asteroides vivos
        currentAsteroids++;

        // Reduz a contagem quando o asteroide for destruído
        asteroid.AddComponent<AsteroidTracker>().Setup(this);
    }

    public void AsteroidDestroyed()
    {
        currentAsteroids--;
    }


}
