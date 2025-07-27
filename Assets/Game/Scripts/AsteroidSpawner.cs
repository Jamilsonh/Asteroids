using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Prefab do asteroide que será instanciado
    public GameObject asteroidPrefab;

    // Pontos de spawn (topo, baixo, esquerda, direita)
    public Transform[] spawnPoints; // 0: Top, 1: Bottom, 2: Left, 3: Right

    // Intervalo entre os spawns
    public float spawnInterval = 15f;

    // Velocidade com que os asteroides se movem
    public float asteroidSpeed = 3f;

    // Temporizador interno
    private float timer;

    // Chamado a cada frame
    void Update()
    {
        // Incrementa o tempo com o deltaTime
        timer += Time.deltaTime;

        // Se passou o tempo definido, spawna um asteroide
        if (timer >= spawnInterval)
        {
            SpawnAsteroid();
            timer = 0f; // Reinicia o temporizador
        }
    }

    // Responsável por instanciar e configurar o asteroide
    void SpawnAsteroid()
    {
        // Escolhe um ponto de spawn aleatório
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        // Define a direção base de acordo com o ponto de origem
        Vector2 baseDirection = Vector2.zero;
        switch (index)
        {
            case 0: baseDirection = Vector2.down; break;   // De cima para baixo
            case 1: baseDirection = Vector2.up; break;     // De baixo para cima
            case 2: baseDirection = Vector2.right; break;  // Da esquerda para a direita
            case 3: baseDirection = Vector2.left; break;   // Da direita para a esquerda
        }

        // Adiciona uma variação angular de até ±30 graus na direção
        float angleOffset = Random.Range(-30f, 30f);
        Vector2 finalDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

        // Instancia o asteroide no ponto escolhido
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPoint.position, Quaternion.identity);

        // Aplica uma escala aleatória no tamanho do asteroide
        float scale = Random.Range(0.8f, 1.5f);
        asteroid.transform.localScale = new Vector3(scale, scale, 1f);

        // Pega o Rigidbody2D e aplica a velocidade
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        rb.linearVelocity = finalDirection.normalized * asteroidSpeed;
    }
}
