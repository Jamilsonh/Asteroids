using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float thrustForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
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
    }
}
