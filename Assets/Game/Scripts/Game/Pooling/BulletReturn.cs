using UnityEngine;

public class BulletReturn : MonoBehaviour
{
    public float lifeTime = 2f; // Tempo de vida antes de voltar para a pool
    private float timer;

    void OnEnable()
    {
        timer = 0f; // Reinicia o contador sempre que a bala é ativada
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
