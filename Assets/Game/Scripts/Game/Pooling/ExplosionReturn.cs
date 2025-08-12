using UnityEngine;

/// <summary>
/// Responsável por detectar quando a partícula termina e devolver o objeto para a pool.
/// </summary>
public class ExplosionReturn : MonoBehaviour
{
    private ParticleSystem ps; // Referência ao sistema de partículas

    void Awake()
    {
        // Obtém a referência ao componente ParticleSystem no mesmo GameObject
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Verifica se a partícula terminou de tocar
        if (!ps.IsAlive())
        {
            // Devolve o objeto para a pool
            ExplosionPool.Instance.ReturnToPool(gameObject);
        }
    }
}
