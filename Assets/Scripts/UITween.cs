using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITween : MonoBehaviour
{
    public float fadeTime = 1.0f;
    public RectTransform rectTransform;

    public float x;
    public float y;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        rectTransform.transform.localPosition = new Vector3(0f, -10f, 0);
        rectTransform.DOAnchorPos(new Vector2(x, y), fadeTime, false).SetEase(Ease.OutElastic);
    }
    void OnEnable()
    {
        rectTransform.anchoredPosition = originalPosition;
        FadeIn();
    }
}
