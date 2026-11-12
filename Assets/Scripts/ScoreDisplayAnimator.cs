using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Puntuación del jugador. El marcador está oculto y aparece con un fundido cada vez que se ganan
/// o se pierden puntos.
/// </summary>
public class ScoreDisplayAnimator : MonoBehaviour
{
    [SerializeField] float currentScore = 0f;
    [SerializeField] float pointsToAdd = 10f;
    [SerializeField] float pointsToSubtract = 2f;
    [SerializeField] TMP_Text pointsText;
    [SerializeField] float animationDuration = 0.5f;
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float displayDuration = 2f;

    private Coroutine animationCoroutine = null;

    /// <summary>Escribe la puntuación y deja el texto invisible.</summary>
    void Start()
    {
        // El texto empieza invisible y solo aparece al ganar o perder puntos
        UpdatePointsText();
        Color color = pointsText.color;
        color.a = 0f;
        pointsText.color = color;
    }

    // Los eventos son estáticos y sobreviven al recargar la escena,
    // así que hay que quitar los listeners al destruir el objeto
    /// <summary>Se suscribe a los eventos de coleccionable y de choque.</summary>
    void OnEnable()
    {
        GameEvents.CollectibleEarned.AddListener(AddPoints);
        GameEvents.ObstacleHit.AddListener(SubtractPoints);
    }

    /// <summary>Se da de baja de los eventos, que son estáticos y siguen vivos al recargar la escena.</summary>
    void OnDisable()
    {
        GameEvents.CollectibleEarned.RemoveListener(AddPoints);
        GameEvents.ObstacleHit.RemoveListener(SubtractPoints);
    }

    /// <summary>Suma <c>pointsToAdd</c> y enseña el marcador.</summary>
    void AddPoints()
    {
        currentScore += pointsToAdd;
        UpdatePointsText();
        TriggerPointsAnimation();
    }

    /// <summary>Resta <c>pointsToSubtract</c> sin bajar de 0 y enseña el marcador.</summary>
    void SubtractPoints()
    {
        currentScore -= pointsToSubtract;
        if (currentScore < 0) currentScore = 0;
        UpdatePointsText();
        TriggerPointsAnimation();
    }

    /// <summary>Escribe la puntuación en el texto.</summary>
    void UpdatePointsText()
    {
        pointsText.text = currentScore.ToString();
    }

    /// <summary>Empieza el fundido, parando el anterior si todavía se estaba haciendo.</summary>
    void TriggerPointsAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(PointsVisibility());
    }

    /// <summary>
    /// Hace aparecer el marcador, lo deja visible <c>displayDuration</c> segundos y lo vuelve a ocultar.
    /// </summary>
    IEnumerator PointsVisibility()
    {
        float elapsedTime = 0f;
        Color startColor = pointsText.color;

        // Fade in
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration; // Normalización simple
            float curveValue = speedCurve.Evaluate(t > 1f ? 1f : t); // Limitar manualmente el valor
            Color newColor = startColor;
            newColor.a = curveValue; // Ajusta la transparencia
            pointsText.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(displayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            float curveValue = speedCurve.Evaluate(t > 1f ? 1f : t);
            Color newColor = startColor;
            newColor.a = 1f - curveValue; // Reduce la transparencia
            pointsText.color = newColor;
            yield return null;
        }

        // Asegúrate de dejar el texto invisible al final
        Color finalColor = pointsText.color;
        finalColor.a = 0f;
        pointsText.color = finalColor;

        animationCoroutine = null;
    }
}