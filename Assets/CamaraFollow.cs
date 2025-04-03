using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CamaraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSmooth = 0.2f;
    [SerializeField] float lookAheadDistance = 30; // mirar hacia adelante distancia
    [SerializeField] float minDistanceToTarget = 5; // Distancia m�nima al objetivo
    [SerializeField] float verticalOffset = 2;
    Vector3 currentVelocity;

    /*
    [SerializeField] float rotationSmooth = 1;
    [SerializeField] float lookAheadSmooth = .2f; // mirar hacia adelante suave
    */
    private void FixedUpdate()
    {
        transform.LookAt(player.transform.position + lookAheadDistance * player.transform.forward);

        Vector3 targetPosition = player.position + minDistanceToTarget * -player.transform.forward + verticalOffset * Vector3.up;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmooth);

        float vectorDistance = (player.position.magnitude - transform.position.magnitude);

        if (vectorDistance < minDistanceToTarget)
        {
            Vector3 directionAwayFromPlayer = (player.position - transform.position);

            Vector3 newPosition = minDistanceToTarget / directionAwayFromPlayer.magnitude * directionAwayFromPlayer;

            transform.position = player.position - directionAwayFromPlayer.normalized * minDistanceToTarget;

        }

        /*
         * vectorDistance: vector que va del player a la c�mara
         * si vectorDistance es menor que minDistanceToTarget:
         *      cambiarle la longitud a vectorDistance pa que sea minDistanceToTarget de largo -> v'
         *      poner la c�mara en v' respecto del player
         */
    }
}

