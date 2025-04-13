using UnityEngine.Events;

public static class GameEvents
{
    // Evento que se dispara al recoger un coleccionable
    public static UnityEvent CollectibleEarned = new UnityEvent();

    // Evento que se dispara al impactar contra un obstáculo
    public static UnityEvent ObstacleHit = new UnityEvent();
}