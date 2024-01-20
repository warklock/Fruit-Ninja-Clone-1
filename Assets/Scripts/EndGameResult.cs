using DG.Tweening;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameResult : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI finalScore;

    public RectTransform indicator;
    [SerializeField] private float indicator_moveTime;
    [SerializeField] private float indicator_x;
    [SerializeField] private float indicator_y;
    [SerializeField] private float indicatorMult;
    [SerializeField] private float indicatorCap;

    public int currentScore_int;
    public int finalScore_int;

    void OnEnable()
    {
        indicator.DOAnchorPos(new Vector2(GameManager.instance.indicatorInitial_x, GameManager.instance.indicatorInitial_y), 0, false);
        currentScore_int = 0;
        finalScore_int = 0;
        finalScore.SetText(finalScore_int.ToString());
        DOTween.To(() => currentScore_int, x => currentScore_int = x, GameManager.instance.score, 2f);
        StartCoroutine(ShowFinalScore());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator ShowFinalScore()
    {
        indicator.DOAnchorPos(new Vector2(GameManager.instance.indicatorInitial_x, GameManager.instance.indicatorInitial_y), 0, false);
        float countTime = 0;
        while (countTime <= 2.5f)
        {
            countTime += Time.deltaTime;
            currentScore.SetText(currentScore_int.ToString());
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        finalScore_int = currentScore_int;
        finalScore.SetText(finalScore_int.ToString());
        indicator_x = indicator.anchoredPosition.x;
        indicator_y = indicator.anchoredPosition.y + (finalScore_int * indicatorMult);
        indicator_y = Mathf.Clamp(indicator_y, -353, indicatorCap);
        indicator.DOAnchorPos(new Vector2(indicator_x, indicator_y), indicator_moveTime, false);
    }
}
