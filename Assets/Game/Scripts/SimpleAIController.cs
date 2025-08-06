using UnityEngine;

public class SimpleAIController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 4f; // Velocidade de movimento
    public float rotationSpeed = 250f; // Velocidade de rota��o (graus por segundo)
    public float moveDurationMin = 1f; // Tempo m�nimo andando
    public float moveDurationMax = 8f; // Tempo m�ximo andando
    public float idleDurationMin = 0.1f; // Tempo m�nimo parado
    public float idleDurationMax = 0.3f; // Tempo m�ximo parado

    [Header("Ataque")]
    public GameObject bulletPrefab; // Prefab do proj�til
    public Transform firePoint; // Ponto de onde sai o tiro
    public float fireCoolwdown = 0.5f; // Tempo entre tiros

    private float stateTimer;
    private bool isMoving;
    private Vector3 rotationDirection;

    private float fireTimer = 0f; // Controle de cooldown

    void Start()
    {
        ChangeState();
    }

    void Update()
    {
        stateTimer -= Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (isMoving)
        {
            // Move para frente
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
        }

        // Rotaciona sempre quando parado (para mudar dire��o)
        if (!isMoving)
        {
            transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }

        // Troca de estado quando o tempo acaba
        if (stateTimer <= 0)
        {
            ChangeState();
        }
    }

    void ChangeState()
    {
        isMoving = !isMoving;
        if (isMoving)
        {
            // Vai andar para frente por um tempo aleat�rio
            stateTimer = Random.Range(moveDurationMin, moveDurationMax);
        }
        else
        {
            // Vai parar e rotacionar para dire��o aleat�ria
            stateTimer = Random.Range(idleDurationMin, idleDurationMax);
            rotationDirection = new Vector3(0, 0, Random.Range(0, 2) == 0 ? 1 : -1); // Esquerda ou Direita
        }
    }

    void Shoot()
    {
        if (fireTimer <= 0f)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            fireTimer = fireCoolwdown;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Shoot();
        }
    }
}
