using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_score : MonoBehaviour
{
    [SerializeField] public int score;
    public TextMeshProUGUI score_Text;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    private void FixedUpdate()
    {
    }

    public virtual void ScoreIncrease()
    {
        score++;
        score_Text.text = score.ToString();
    }
}
