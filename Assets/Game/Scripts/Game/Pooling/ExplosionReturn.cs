using UnityEngine;

/// <summary>
/// Respons�vel por detectar quando a part�cula termina e devolver o objeto para a pool.
/// </summary>
public class ExplosionReturn : MonoBehaviour
{
    private ParticleSystem ps; // Refer�ncia ao sistema de part�culas

    void Awake()
    {
        // Obt�m a refer�ncia ao componente ParticleSystem no mesmo GameObject
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Verifica se a part�cula terminou de tocar
        if (!ps.IsAlive())
        {
            // Devolve o objeto para a pool
            ExplosionPool.Instance.ReturnToPool(gameObject);
        }
    }
}
