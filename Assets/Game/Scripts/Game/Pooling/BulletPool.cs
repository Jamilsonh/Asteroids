using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance; // Singleton para acesso rápido
    public GameObject bulletPrefab;    // Prefab da bala
    public int initialSize = 20;       // Quantidade inicial de balas

    private Queue<GameObject> pool;    // Fila de balas disponíveis

    void Awake()
    {
        Instance = this;
        pool = new Queue<GameObject>();

        // Cria a pool inicial
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Obtém uma bala da pool.
    /// </summary>
    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        // Se houver balas disponíveis, pega uma da fila
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            // Se a pool estiver vazia, cria uma nova
            obj = Instantiate(bulletPrefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// Devolve a bala para a pool.
    /// </summary>
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
