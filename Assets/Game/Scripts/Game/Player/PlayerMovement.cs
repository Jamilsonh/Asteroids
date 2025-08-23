using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float thrustForce = 5f;
    public float maxSpeed = 10f; // Velocidade máxima

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float rotationInput = -Input.GetAxisRaw("Horizontal");
        rb.MoveRotation(rb.rotation + rotationInput * rotationSpeed * Time.fixedDeltaTime);

        // IMPULSO NA DIREÇÃO ATUAL
        float thrustInput = Input.GetAxisRaw("Vertical");
        if (thrustInput > 0)
        {
            rb.AddForce(transform.up * thrustInput * thrustForce);
        }

        // FREIO
        if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity *= 0.95f;
        }

        // LIMITA VELOCIDADE MÁXIMA
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
