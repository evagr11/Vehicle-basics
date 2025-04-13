using System.Collections;
using TMPro;
using UnityEngine;

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

    void Start()
    {
        GameEvents.CollectibleEarned.AddListener(AddPoints);
        GameEvents.ObstacleHit.AddListener(SubtractPoints);
    }

    void AddPoints()
    {
        currentScore += pointsToAdd;
        UpdatePointsText();
        TriggerPointsAnimation();
    }

    void SubtractPoints()
    {
        currentScore -= pointsToSubtract;
        if (currentScore < 0) currentScore = 0;
        UpdatePointsText();
        TriggerPointsAnimation();
    }

    void UpdatePointsText()
    {
        pointsText.text = currentScore.ToString();
    }

    void TriggerPointsAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(PointsVisibility());
    }

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