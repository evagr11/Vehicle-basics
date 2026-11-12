using UnityEngine;

/// <summary>Coleccionable que gira sobre sí mismo y da puntos al recogerlo.</summary>
public class CollectibleBehaviour : MonoBehaviour
{
    /// <summary>Grados por segundo que gira.</summary>
    public float rotationSpeed = 60f;

    /// <summary>Gira el coleccionable.</summary>
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Si lo toca el jugador, lanza <see cref="GameEvents.CollectibleEarned"/>, pide otro al
    /// <see cref="CollectableSpawner"/> y se destruye.
    /// </summary>
    /// <param name="other">Collider que ha entrado.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Dispara el evento de coleccionable ganado.
            GameEvents.CollectibleEarned.Invoke();

            // Busca el spawner para generar un nuevo coleccionable
            CollectableSpawner spawner = Object.FindFirstObjectByType<CollectableSpawner>();
            if (spawner != null)
            {
                spawner.Spawn();
            }
            Destroy(gameObject);
        }
    }
}
