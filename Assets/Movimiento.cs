using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Valores marcha alante")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotationSpeed;

    [Header("Valores fuerza")]
    [SerializeField] float friction;
    [SerializeField] float fuerzaFreno;

    private bool accelerating;
    private bool deccelerating;
    private float rotationInput;
    private bool handbrake;

    private TrailRenderer trail;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponentInChildren<TrailRenderer>();
    }

    void Update()
    {
        movementWithInput();
    }

    void FixedUpdate()
    {
        MovementPhysics();
    }

    //Movimiento y rotaci�n del personaje
    void movementWithInput()
    {
        rotationInput = 0;

        accelerating = Input.GetKey(KeyCode.W);

        deccelerating = Input.GetKey(KeyCode.S);

        handbrake = Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1;
        }

    }

    // Controla la f�sica del movimiento y aplica fricci�n
    void MovementPhysics()
    {
        rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity); // Vector del espacio global a un vector en el espacio local del personaje

        if (accelerating)
        {
            if (localVelocity.z < maxSpeed)
            {
                rb.velocity += speed * transform.forward * Time.fixedDeltaTime;
            }
        }
        else
        {
            // Se aplica la fricci�n para reducir la velocidad gradualmente
            rb.velocity = new Vector3(
                rb.velocity.x * (1 / (1 + friction * Time.fixedDeltaTime)),
                0,
                rb.velocity.z * (1 / (1 + friction * Time.fixedDeltaTime))
            );
        }

        if (deccelerating)
        {
            if (localVelocity.z > 0) // Si va hacia adelante
            {
                Brake(); // Aplica el freno
            }
            else // Si no lo acelera en sentido contrario
            {

                rb.velocity -= speed * transform.forward * Time.fixedDeltaTime;
            }
        }
       
        if(handbrake)
        {
            ApplyHandbrake();
        }
        else
        {
            // Desactiva el Trail Renderer cuando no est� activado el freno de mano
            trail.emitting = false;
        }

        // Limita la velocidad m�xima
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        // Limita la velocidad de rotaci�n m�xima
        if (rb.angularVelocity.magnitude > maxRotationSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxRotationSpeed;
        }

    }

    void Brake()
    {
        rb.velocity = new Vector3(
            rb.velocity.x * (1 / (1 + fuerzaFreno * Time.fixedDeltaTime)),
            0,
            rb.velocity.z * (1 / (1 + fuerzaFreno * Time.fixedDeltaTime))
        );
    }

    void ApplyHandbrake()
    {
        float handbrakeFuerza = 5f; 
        float extraRotation = 2f; // Aceleraci�n angular extra si est� girando

        rb.velocity = new Vector3(
            rb.velocity.x * (1 / (1 + handbrakeFuerza * Time.fixedDeltaTime)),
            0,
            rb.velocity.z * (1 / (1 + handbrakeFuerza * Time.fixedDeltaTime))
        );

        // Si el coche est� girando, aumentar la rotaci�n 
        if (rotationInput != 0)
        {
            rb.AddTorque(rotationInput * extraRotation * transform.up);

        }

        // Desactiva el Trail Renderer cuando est� activado el freno de mano
        trail.emitting = true;
    }
}

