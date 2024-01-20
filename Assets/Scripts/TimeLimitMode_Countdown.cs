using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeLimitMode_Countdown : MonoBehaviour
{
    public float seconds;
    public float minutes;

    public TextMeshProUGUI secondsTMP;
    public TextMeshProUGUI minutesTMP;

    private bool onetime = false;

    void Start()
    {
        minutes = GameManager.instance.timeLimitModeDuration / 60;
        seconds = GameManager.instance.timeLimitModeDuration % 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Pomegranate.zooming == false && GameManager.instance.frezzing == false) { GameManager.instance.timeLimitModeDuration -= Time.fixedDeltaTime; }
        GameManager.instance.timeLimitModeDuration = Mathf.Clamp(GameManager.instance.timeLimitModeDuration,0,999);
        minutes = GameManager.instance.timeLimitModeDuration / 60;
        seconds = GameManager.instance.timeLimitModeDuration % 60;

        if (seconds < 10f)
        {
            secondsTMP.SetText("0" + Mathf.FloorToInt(seconds).ToString());
        }
        else
        {
            secondsTMP.SetText(Mathf.FloorToInt(seconds).ToString());
        }

        if (minutes < 10f)
        {
            minutesTMP.SetText("0" + Mathf.FloorToInt(minutes).ToString() + " :");
        }
        else
        {
            minutesTMP.SetText(Mathf.FloorToInt(minutes).ToString() + " :");
        }
        if (GameManager.instance.timeLimitModeDuration > 0)
        {
            onetime = false;
        }
        if (GameManager.instance.timeLimitModeDuration <= 0 && onetime == false)
        {
            onetime = true;
            GameManager.instance.EndGameMethod();
        }
    }
}
