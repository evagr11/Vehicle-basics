using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Obstáculo que sube y baja entre dos puntos siguiendo una curva. Si el coche choca con él,
/// resta puntos y reinicia la escena.
/// </summary>
public class Obstacles : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] float speed = 2f;

    private bool goingUp = true;
    private float elapsedTime = 0f;
    private bool hitPlayer = false;

    /// <summary>Mueve el obstáculo hacia arriba o hacia abajo y cambia de sentido al llegar a un extremo.</summary>
    void Update()
    {
        elapsedTime += Time.deltaTime * speed;

        // Distancia entre los puntos A y B.
        float distance = pointB.position.y - pointA.position.y;

        // Calcula el valor normalizado de tiempo y evalúa la curva.
        float t = elapsedTime / distance;
        float curveT = movementCurve.Evaluate(t);

        if (goingUp)
        {
            transform.position = new Vector3(transform.position.x, pointA.position.y + curveT * distance, transform.position.z);

            if (transform.position.y >= pointB.position.y)
            {
                goingUp = false;
                elapsedTime = 0f;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, pointB.position.y - curveT * distance, transform.position.z);
            if (transform.position.y <= pointA.position.y)
            {
                goingUp = true;
                elapsedTime = 0f;
            }
        }
    }

    // Detecta colisiones con el jugador
    /// <summary>
    /// Al chocar con el jugador lanza <see cref="GameEvents.ObstacleHit"/> una sola vez y reinicia la escena al segundo.
    /// </summary>
    /// <param name="collision">Datos del choque.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // hitPlayer evita restar puntos varias veces mientras se espera al reinicio
        if (!hitPlayer && collision.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
            // Dispara el evento para obstáculo golpeado
            GameEvents.ObstacleHit.Invoke();
            Invoke(nameof(ResetScene), 1f);
        }
    }

    /// <summary>Vuelve a cargar la escena actual.</summary>
    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}