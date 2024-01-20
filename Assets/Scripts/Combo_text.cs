using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Combo_text : MonoBehaviour
{
    public float destroy_Time = 1f;
    public float maxScale;
    public float scale_step;

    float countTime;
    public Vector3 initialScale;

    void Start()
    {
        Destroy(gameObject, destroy_Time);
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (countTime >= 0.08f)
        {
            transform.localScale = initialScale;
        }
        else
        {
            transform.localScale = transform.localScale + new Vector3(scale_step, scale_step, scale_step);
            countTime += Time.deltaTime;
        }
        transform.position = transform.position + new Vector3(0, 0.01f, 0);
    }
}
