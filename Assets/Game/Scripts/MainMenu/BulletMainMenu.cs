using UnityEngine;

public class BulletMainMenu : MonoBehaviour
{
    public float speed = 20f;     // Velocidade da bala
    public float lifeTime = 3f;   // Dura��o antes de sumir

    void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destrui��o caso n�o acerte nada
    }

    void Update()
    {
        // Move para frente (local Up)
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject); // S� destroi a bala
        }
    }
}
