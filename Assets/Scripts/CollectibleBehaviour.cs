using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public float rotationSpeed = 60f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

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
