using UnityEngine;

public class BulletReturn : MonoBehaviour
{
    public float lifeTime = 2f;
    private float timer;

    private static int globalID = 0;  // contador global de balas
    private int bulletID;

    void Awake()
    {
        bulletID = globalID;  // cada bala ganha um ID único
        globalID++;
    }

    void OnEnable()
    {
        timer = 0f;
        Debug.Log("Ativando bala ID: " + bulletID);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            BulletPool.Instance.ReturnToPool(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BulletPool.Instance.ReturnToPool(gameObject);
        }
    }

}
