using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control del coche con físicas: W acelera, S frena o va marcha atrás, A y D giran y Espacio es
/// el freno de mano, que derrapa y activa las estelas de las ruedas.
/// </summary>
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


    [Header("Trails")]
    [SerializeField] private List<TrailRenderer> trailRenderer;

    private bool accelerating;
    private bool deccelerating;
    private float rotationInput;
    private bool handbrake;

    private Rigidbody rb;

    /// <summary>Guarda el <c>Rigidbody</c>.</summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>Lee el teclado en cada fotograma.</summary>
    void Update()
    {
        movementWithInput();
    }

    /// <summary>Aplica la física en cada paso de física.</summary>
    void FixedUpdate()
    {
        MovementPhysics();
    }

    //Movimiento y rotación del personaje
    /// <summary>Guarda qué teclas están pulsadas: acelerar, frenar, freno de mano y dirección de giro.</summary>
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

    // Controla la física del movimiento y aplica fricción
    /// <summary>
    /// Gira el coche, acelera o aplica la fricción, frena o va marcha atrás, aplica el freno de mano
    /// y limita la velocidad y el giro máximos.
    /// </summary>
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
            // Se aplica la fricción para reducir la velocidad gradualmente
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
            // Desactiva el Trail Renderer cuando no está activado el freno de mano
            foreach (TrailRenderer trail in trailRenderer)
            {
                trail.emitting = false;
            }
        }

        // Limita la velocidad máxima
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        // Limita la velocidad de rotación máxima
        if (rb.angularVelocity.magnitude > maxRotationSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxRotationSpeed;
        }

    }

    /// <summary>Reduce la velocidad con la fuerza de freno.</summary>
    void Brake()
    {
        rb.velocity = new Vector3(
            rb.velocity.x * (1 / (1 + fuerzaFreno * Time.fixedDeltaTime)),
            0,
            rb.velocity.z * (1 / (1 + fuerzaFreno * Time.fixedDeltaTime))
        );
    }

    /// <summary>Frena con fuerza, gira más si se está girando y activa las estelas de las ruedas.</summary>
    void ApplyHandbrake()
    {
        float handbrakeFuerza = 15f; 
        float extraRotation = 2f; // Aceleración angular extra si está girando

        rb.velocity = new Vector3(
            rb.velocity.x * (1 / (1 + handbrakeFuerza * Time.fixedDeltaTime)),
            0,
            rb.velocity.z * (1 / (1 + handbrakeFuerza * Time.fixedDeltaTime))
        );

        // Si el coche está girando, aumentar la rotación 
        if (rotationInput != 0)
        {
            rb.AddTorque(rotationInput * extraRotation * transform.up);

        }

        foreach (TrailRenderer trail in trailRenderer)
        {
            trail.emitting = true; 
        }
    }
}

