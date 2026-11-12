using UnityEngine;

/// <summary>
/// Cámara en tercera persona que sigue al coche desde detrás y mira un poco por delante de él.
/// </summary>
public class CamaraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followSmooth = 0.2f;
    [SerializeField] float lookAheadDistance = 30; // mirar hacia adelante distancia
    [SerializeField] float minDistanceToTarget = 5; // Distancia mínima al objetivo
    [SerializeField] float verticalOffset = 2;
    Vector3 currentVelocity;

    /// <summary>
    /// Mira hacia un punto delante del coche, se mueve suavemente detrás y por encima de él, y nunca
    /// se acerca más de <c>minDistanceToTarget</c>.
    /// </summary>
    private void FixedUpdate()
    {
        transform.LookAt(player.transform.position + lookAheadDistance * player.transform.forward);

        Vector3 targetPosition = player.position + minDistanceToTarget * -player.transform.forward + verticalOffset * Vector3.up;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, followSmooth);

        // Si la cámara se acerca demasiado al jugador, la separa hasta la distancia mínima
        Vector3 directionToPlayer = player.position - transform.position;

        if (directionToPlayer.magnitude < minDistanceToTarget)
        {
            transform.position = player.position - directionToPlayer.normalized * minDistanceToTarget;
        }
    }
}

