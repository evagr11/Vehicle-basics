using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    [SerializeField] List<Transform> obstaculos; 
    [SerializeField] string tagJugador = "Player"; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagJugador))
        {
            MoverColeccionable();
        }
    }

    void MoverColeccionable()
    {
        // obst�culo aleatorio de la lista de obst�culos (entre 0, y el numero de item que tenga la lista)
        int indiceAleatorio = Random.Range(0, obstaculos.Count);

        // Obtener la nueva posici�n aleatoria
        Transform nuevoObstaculo = obstaculos[indiceAleatorio];

        // coleccionable a la posici�n del nuevo obst�culo, manteniendo Y = 1
        transform.position = new Vector3(nuevoObstaculo.position.x, 1f, nuevoObstaculo.position.z);

        print("Coleccionable recogido");
    }
}   
