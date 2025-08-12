using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance; // Singleton para acesso global

    [Header("Configuração do Pool")]
    public GameObject explosionPrefab; // Prefab de explosão a ser reutilizado
    public int initialSize = 10;       // Número de explosões pré-criadas no início

    private Queue<GameObject> pool;    // Fila de objetos disponíveis para uso

    void Awake()
    {
        // Inicializa a instância Singleton
        Instance = this;

        // Cria a fila (pool) de objetos
        pool = new Queue<GameObject>();

        // Pré-instancia 'initialSize' explosões desativadas
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Obtém uma explosão da pool, posiciona, ativa e reinicia o sistema de partículas.
    /// </summary>
    public GameObject GetExplosion(Vector3 position)
    {
        GameObject obj;

        // Se houver explosões disponíveis na pool, reutiliza
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            // Se a pool estiver vazia, instancia uma nova explosão
            obj = Instantiate(explosionPrefab);
        }

        // Posiciona a explosão no local desejado
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);

        // Reinicia o sistema de partículas (caso exista)
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear(); // Limpa qualquer estado anterior
            ps.Play();  // Toca novamente
        }

        return obj;
    }

    /// <summary>
    /// Devolve a explosão para a pool, desativando e enfileirando.
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
