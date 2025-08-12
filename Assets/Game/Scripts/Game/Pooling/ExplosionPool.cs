using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance; // Singleton para acesso global

    [Header("Configura��o do Pool")]
    public GameObject explosionPrefab; // Prefab de explos�o a ser reutilizado
    public int initialSize = 10;       // N�mero de explos�es pr�-criadas no in�cio

    private Queue<GameObject> pool;    // Fila de objetos dispon�veis para uso

    void Awake()
    {
        // Inicializa a inst�ncia Singleton
        Instance = this;

        // Cria a fila (pool) de objetos
        pool = new Queue<GameObject>();

        // Pr�-instancia 'initialSize' explos�es desativadas
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Obt�m uma explos�o da pool, posiciona, ativa e reinicia o sistema de part�culas.
    /// </summary>
    public GameObject GetExplosion(Vector3 position)
    {
        GameObject obj;

        // Se houver explos�es dispon�veis na pool, reutiliza
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            // Se a pool estiver vazia, instancia uma nova explos�o
            obj = Instantiate(explosionPrefab);
        }

        // Posiciona a explos�o no local desejado
        obj.transform.position = position;
        obj.transform.rotation = Quaternion.identity;
        obj.SetActive(true);

        // Reinicia o sistema de part�culas (caso exista)
        ParticleSystem ps = obj.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear(); // Limpa qualquer estado anterior
            ps.Play();  // Toca novamente
        }

        return obj;
    }

    /// <summary>
    /// Devolve a explos�o para a pool, desativando e enfileirando.
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
