using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    public AudioClip[] explosionSounds;
    public GameObject explosionEffect;

    private WaveData waveData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //float randomTorque = Random.Range(-10f, 10f);
        //GetComponent<Rigidbody2D>().AddTorque(randomTorque);
        //Destroy(gameObject, lifeTime);
    }

    public void Setup(WaveData data)
    {
        waveData = data;
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

            // usa o valor do ScriptableObject
            if (waveData != null)
            {
                ScoreManager.Instance.AddScore(waveData.fragmentAsteroidScore);
            }
            else
            {
                Debug.LogWarning("WaveData não configurado no SmallAsteroid!");
            }

            BulletPool.Instance.ReturnToPool(other.gameObject);
            Destroy(gameObject);
        }
    }
}