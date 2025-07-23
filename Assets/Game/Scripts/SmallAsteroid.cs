using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public float lifeTime = 10f;
    public AudioClip[] explosionSounds;
    public GameObject explosionEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomTorque = Random.Range(-10f, 10f);
        GetComponent<Rigidbody2D>().AddTorque(randomTorque);
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            if (explosionSounds.Length > 0)
            {
                int index = Random.Range(0, explosionSounds.Length);
                AudioClip clip = explosionSounds[index];
                AudioSource.PlayClipAtPoint(clip, transform.position);
            }

            ScoreManager.Instance.AddScore(5); // valor maior se quiser recompensar fragmentos
            Destroy(other.gameObject); // Destroi a bala
            Destroy(gameObject);
        }
        
       
    }   
}
