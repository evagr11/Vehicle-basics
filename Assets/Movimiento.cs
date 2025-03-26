using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float friction;
    [SerializeField] float fuerzaRotacion;
    private Rigidbody rb;
    private Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(0, -fuerzaRotacion, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(0, fuerzaRotacion, 0);
        }

        // Añade velocidad según la dirección
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += direction.normalized * speed * Time.deltaTime;
        }

        if (direction == Vector3.zero)
        {
            velocity = new Vector3(
                velocity.x * (1 - friction * Time.deltaTime),
                velocity.y,
                velocity.z * (1 - friction * Time.deltaTime)
            );
        }
    }


}

