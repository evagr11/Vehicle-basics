using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotationSpeed;
    [SerializeField] float friction;
    [SerializeField] float fuerzaFreno;
    private Rigidbody rb;

    private bool accelerating;
    private bool deccelerating;
    private float rotationInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementWithInput();
    }

    void FixedUpdate()
    {
        MovementPhysics();
    }

    //Movimiento y rotación del personaje
    void movementWithInput()
    {
        rotationInput = 0;

        accelerating = Input.GetKey(KeyCode.W);

        deccelerating = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1;
        }

        rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);
    }

    // Controla la física del movimiento y aplica fricción
    void MovementPhysics()
    {
        Vector3 forwardDirection = transform.forward; // Dirección del movimiento

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity); // Vector del espacio global a un vector en el espacio local del personaje

        if (accelerating)
        {
            transform.TransformDirection(rb.velocity); // Vector en el espacio local del personaje a un vector en el espacio global
            rb.velocity += speed * forwardDirection * Time.fixedDeltaTime;
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
            transform.TransformDirection(rb.velocity);

            if (localVelocity.z > 0) // Si va hacia adelante
            {
                Brake(); // Aplica el freno
            }
            else // Si no lo acelera en sentido contrario
            {

                rb.velocity -= speed * forwardDirection * Time.fixedDeltaTime;
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

    void Brake()
    {
        rb.velocity = new Vector3(
            rb.velocity.x * (1 / (1 + fuerzaFreno * Time.fixedDeltaTime)),
            0,
            rb.velocity.z * (1 / (1 + fuerzaFreno * Time.fixedDeltaTime))
        );
    }
}

