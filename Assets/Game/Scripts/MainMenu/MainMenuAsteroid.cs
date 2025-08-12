using UnityEngine;

public class MainMenuAsteroid : MonoBehaviour
{
    public GameObject explosionEffect; // Prefab da explos�o (part�culas)

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se colidiu com a bala
        if (other.CompareTag("Bullet"))
        {
            // Instancia a explos�o na posi��o do asteroide
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject); // Destroi a bala
            Destroy(gameObject); // Destroi o asteroide
        }
    }
}
