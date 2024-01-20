using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int score;

    private float countTime = 0;
    public float duration= 3f;
    public static int comboCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (GameManager.instance != null && GameManager.instance.startComboCounting == true)
        {
            StartCounting();
        }
    }

    public void StartCounting()
    {
        countTime += Time.deltaTime;
        if (countTime >= GameManager.instance.comboDuration)
        {
            countTime = 0;
        }
    }
}
