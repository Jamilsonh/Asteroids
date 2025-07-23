using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Tempo de vida do asteroide antes de ser destruído automaticamente
    public float lifeTime = 10f;
    public GameObject explosionEffect; // Prefab de explosão opcional
    public GameObject smallAsteroidPrefab;

    public AudioClip[] explosionSounds;

    void Start()
    {
        // Gera um torque (rotação) aleatório entre -10 e 10
        float randomTorque = Random.Range(-10f, 10f);

        // Aplica esse torque ao Rigidbody2D para fazer o asteroide girar
        GetComponent<Rigidbody2D>().AddTorque(randomTorque);

        // Destroi automaticamente o objeto após 'lifeTime' segundos
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com a bala
        if (other.CompareTag("Bullet"))
        {
            // Instacia efeito de explosão (opcional)
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            if (smallAsteroidPrefab != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject fragment = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);

                    // Aplica uma força em direções aleatórias
                    Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 direction = Random.insideUnitCircle.normalized;
                        float speed = Random.Range(0.2f, 0.6f);
                        rb.linearVelocity = direction * speed;
                    }
                }
            }
            
            if (explosionSounds.Length > 0)
            {
                int index = Random.Range(0, explosionSounds.Length);
                AudioClip clip = explosionSounds[index];
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }

            ScoreManager.Instance.AddScore(10); // por exemplo

            // Destroi o asteroide
            Destroy(other.gameObject); // Destroi a bala
            Destroy(gameObject);
        }
    }
}