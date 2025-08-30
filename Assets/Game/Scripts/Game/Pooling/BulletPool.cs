using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    public int initialSize = 20;
    private Queue<GameObject> pool;

    void Awake()
    {
        Instance = this;
        pool = new Queue<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        // Se não houver balas disponíveis, cria uma nova
        if (pool.Count == 0)
        {
            obj = Instantiate(bulletPrefab);
        }
        else
        {
            obj = pool.Dequeue();
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        // Garante que a bala não está duplicada na pool
        if (!pool.Contains(obj))
        {
            pool.Enqueue(obj);
        }
    }
}
