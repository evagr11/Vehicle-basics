using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSmooth = 0.2f;
    [SerializeField] float rotationSmooth = 1;
    [SerializeField] float lookAheadDistance = 30; // mirar hacia adelante distancia
    [SerializeField] float lookAheadSmooth = .2f; // mirar hacia adelante suave
    [SerializeField] float minDistanceToTarget = 5; // Distancia mínima al objetivo
    [SerializeField] float verticalOffset = 2;

    Vector3 currentVelocity;

    private void FixedUpdate()
    {
        transform.LookAt(player.transform);

        Vector3 targetPosition = player.position + minDistanceToTarget * -player.transform.forward + verticalOffset * Vector3.up;

        float vectorDistance = (player.position.magnitude - transform.position.magnitude);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmooth);

        if (vectorDistance < minDistanceToTarget)
        {
            Vector3 directionAwayFromPlayer = (player.position - transform.position).normalized;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmooth);


        }

        /*
         * vectorDistance: vector que va del player a la cámara
         * si vectorDistance es menor que minDistanceToTarget:
         *      cambiarle la longitud a vectorDistance pa que sea minDistanceToTarget de largo -> v'
         *      poner la cámara en v' respecto del player
         */
    }
}

