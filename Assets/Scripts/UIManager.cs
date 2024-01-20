using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public float fadeTime = 1.0f;
    public CanvasGroup[] canvasGroup;
    public RectTransform[] rectTransform;

    public List<GameObject> panels = new List<GameObject>();
    private Vector2[] originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        PanelFadeIn(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelFadeIn(int Index)
    {
        rectTransform[Index].transform.localPosition = new Vector3(0f, 0f, 0);
        rectTransform[Index].DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup[Index].alpha = 0f;
        canvasGroup[Index].DOFade(1, fadeTime);
    }

    public void PanelFadeOut(int Index)
    {
        canvasGroup[Index].alpha = 1f;
        rectTransform[Index].transform.localPosition = new Vector3(0f, 0f, 0);
        rectTransform[Index].DOAnchorPos(new Vector2(0f, -1000f), fadeTime, false).SetEase(Ease.InOutQuint);
        canvasGroup[Index].DOFade(0, fadeTime);
    }
}
