using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Tempo de vida do asteroide antes de ser destru�do automaticamente
    public float lifeTime = 10f;
    public GameObject explosionEffect; // Prefab de explos�o opcional
    public GameObject smallAsteroidPrefab;

    public AudioClip[] explosionSounds;

    void Start()
    {
        // Gera um torque (rota��o) aleat�rio entre -10 e 10
        float randomTorque = Random.Range(-10f, 10f);

        // Aplica esse torque ao Rigidbody2D para fazer o asteroide girar
        GetComponent<Rigidbody2D>().AddTorque(randomTorque);

        // Destroi automaticamente o objeto ap�s 'lifeTime' segundos
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com a bala
        if (other.CompareTag("Bullet"))
        {
            // Instacia efeito de explos�o (opcional)
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            if (smallAsteroidPrefab != null) // Verifica se o prefab do asteroide pequeno est� atribu�do
            {
                // Obt�m a escala absoluta (em X) do asteroide atual (o que explodiu)
                float parentScale = transform.localScale.x;

                // Fator aleat�rio para aumentar a escala total dos fragmentos
                // (2.5 a 2.8 vezes a escala do pai) � escala visual maior
                float scaleFactor = Random.Range(2.5f, 2.8f);

                // Calcula a soma total de escala que ser� dividida entre os fragmentos
                float totalScale = parentScale * scaleFactor;

                // Gera um valor de propor��o entre 30% e 70% para o primeiro fragmento
                // O segundo recebe o restante (1 - ratio)
                float ratio = Random.Range(0.3f, 0.7f);
                float scaleA = totalScale * ratio;         // Escala do primeiro fragmento
                float scaleB = totalScale * (1f - ratio);  // Escala do segundo fragmento

                // Armazena os dois valores de escala em um array para facilitar o uso no loop
                float[] scales = new float[] { scaleA, scaleB };

                // Loop para instanciar os dois fragmentos
                for (int i = 0; i < 2; i++)
                {
                    // Instancia o fragmento na mesma posi��o do asteroide original
                    GameObject fragment = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);

                    // Define a escala absoluta do fragmento (sem depender da escala original do prefab)
                    float finalScale = scales[i];
                    fragment.transform.localScale = new Vector3(finalScale, finalScale, finalScale);

                    // Aplica f�sica ao fragmento
                    Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        // Dire��o aleat�ria de movimenta��o
                        Vector2 direction = Random.insideUnitCircle.normalized;

                        // Velocidade aleat�ria dentro do intervalo
                        float speed = Random.Range(0.1f, 2f);
                        rb.linearVelocity = direction * speed;

                        // Torque (rota��o) aleat�rio
                        float randomTorque = Random.Range(-30f, 30f);
                        rb.AddTorque(randomTorque);
                    }

                    // LOGICA DE AUMENTO DE VELOCIDADE DO ASTEROID APOS EXPLODIR BASEADO EM WAVES (IMPLEMENTA��O FUTURA)
                    
                    //if (rb != null)
                    //{
                    //    Vector2 direction = Random.insideUnitCircle.normalized;

                    //    // Velocidade depende da wave atual
                    //    float speed;
                    //    int currentWave = WaveManager.Instance.CurrentWave;

                    //    if (currentWave < 5)
                    //    {
                    //        // Est�gio 1: fragmentos lentos
                    //        speed = Random.Range(0.1f, 0.5f);
                    //    }
                    //    else if (currentWave < 10)
                    //    {
                    //        // Est�gio 2: velocidade m�dia
                    //        speed = Random.Range(0.4f, 1.2f);
                    //    }
                    //    else
                    //    {
                    //        // Est�gio 3: r�pido
                    //        speed = Random.Range(1.0f, 2.0f);
                    //    }

                    //    rb.linearVelocity = direction * speed;

                    //    // Torque (rota��o aleatoria
                    //    float randomTorque = Random.Range(-30f, 30f);
                    //    rb.AddTorque(randomTorque);
                    //}

                    // Destr�i o fragmento automaticamente ap�s 10 segundos
                    Destroy(fragment, 10f);
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