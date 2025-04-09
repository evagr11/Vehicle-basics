using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    [SerializeField] Transform suelo;
    [SerializeField] Transform subida;
    [SerializeField] AnimationCurve curvaMovimiento;
    [SerializeField] float velocidad = 2f;

    private bool subiendo = true;
    private float tiempoRecorrido = 0f;

    void Update()
    {
        tiempoRecorrido += Time.deltaTime * velocidad;

        // distancia entre los puntos suelo y subida
        float distancia = subida.position.y - suelo.position.y;

        // Hace la curva con el valor del tiempo normalizado
        float t = tiempoRecorrido / distancia;
        float curvaT = curvaMovimiento.Evaluate(t);

        if (subiendo)
        {
            // Movimiento de la columna entre suelo y subida usando el valor de la curva
            transform.position = new Vector3(transform.position.x, suelo.position.y + curvaT * distancia, transform.position.z);

            // Si llega a B, cambia la dirección y reinicia el tiempo de recorrido
            if (transform.position.y >= subida.position.y)
            {
                subiendo = false;
                tiempoRecorrido = 0f;
            }
        }
        else
        {
            // Movimiento de la columna entre subida y suelo usando el valor de la curva
            transform.position = new Vector3(transform.position.x, subida.position.y - curvaT * distancia, transform.position.z);

            // Si llega a suelo, cambia la dirección y reinicia el tiempo de recorrido
            if (transform.position.y <= suelo.position.y)
            {
                subiendo = true;
                tiempoRecorrido = 0f;
            }
        }
    }

    // Detecta colisiones con el jugador
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Colisión con jugador");
            // Reinicia la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
