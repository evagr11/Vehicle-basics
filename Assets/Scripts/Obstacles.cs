using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] float speed = 2f;

    private bool goingUp = true;
    private float elapsedTime = 0f;

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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Dispara el evento para obstáculo golpeado
            GameEvents.ObstacleHit.Invoke();
            Invoke("ResetScene", 1f);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}