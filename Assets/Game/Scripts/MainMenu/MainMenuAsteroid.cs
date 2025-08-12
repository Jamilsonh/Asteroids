using UnityEngine;

public class MainMenuAsteroid : MonoBehaviour
{
    public GameObject explosionEffect; // Prefab da explosão (partículas)

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se colidiu com a bala
        if (other.CompareTag("Bullet"))
        {
            // Instancia a explosão na posição do asteroide
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject); // Destroi a bala
            Destroy(gameObject); // Destroi o asteroide
        }
    }
}
