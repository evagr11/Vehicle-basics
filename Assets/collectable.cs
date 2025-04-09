using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    [SerializeField] List<Transform> obstaculos; 
    int indiceAleatorio = 1;
        int newIndiceAleatorio;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            MoverColeccionable();
        }
    }

    void MoverColeccionable()
    {

        // obstáculo aleatorio de la lista de obstáculos (entre 0, y el numero de item que tenga la lista)
        do
        {
            indiceAleatorio = Random.Range(0, obstaculos.Count);

        } while (indiceAleatorio == newIndiceAleatorio);


        // Obtener la nueva posición aleatoria
        Transform nuevoObstaculo = obstaculos[indiceAleatorio];

        // coleccionable a la posición del nuevo obstáculo, manteniendo Y = 1
        transform.position = new Vector3(nuevoObstaculo.position.x, 1f, nuevoObstaculo.position.z);

        print("Coleccionable recogido");
    }
}   
