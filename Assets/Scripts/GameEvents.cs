using UnityEngine.Events;

/// <summary>Eventos del juego que conectan los coleccionables y los obstáculos con la puntuación.</summary>
public static class GameEvents
{
    // Evento que se dispara al recoger un coleccionable
    /// <summary>Se lanza al recoger un coleccionable.</summary>
    public static UnityEvent CollectibleEarned = new UnityEvent();

    // Evento que se dispara al impactar contra un obstáculo
    /// <summary>Se lanza al chocar contra un obstáculo.</summary>
    public static UnityEvent ObstacleHit = new UnityEvent();
}