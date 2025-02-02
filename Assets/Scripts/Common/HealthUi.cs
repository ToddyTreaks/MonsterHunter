using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUi : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private RectTransform _smooRectTransform;

    private int tempsDeDeplacement = 2;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(1, 1);
        _smooRectTransform.anchorMax = new Vector2(1, 1);
    }

    public void UpdateFill(float health, float maxhealth)
    {
        if (maxhealth != 0)
        {
            var rectTarget = new Vector2((float)health / (float)maxhealth, 1);
            rectTransform.anchorMax = rectTarget;
            StartCoroutine(FillSmooth(_smooRectTransform.anchorMax, rectTarget));
        }
    }

    IEnumerator FillSmooth(Vector2 pos,Vector2 destination)
    {
        float elapsedTime = 0f;
        Vector2 positionInitiale = pos;

        while (elapsedTime < tempsDeDeplacement)
        {
            _smooRectTransform.anchorMax = Vector2.Lerp(positionInitiale, destination, elapsedTime / tempsDeDeplacement);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _smooRectTransform.anchorMax = destination;
        yield return null;
    }
}
