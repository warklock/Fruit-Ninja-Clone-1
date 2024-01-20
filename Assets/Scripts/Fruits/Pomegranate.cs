using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using VFX;
using static UnityEngine.GraphicsBuffer;

public class Pomegranate : Fruits
{
    [SerializeField] private GameObject upper1;
    [SerializeField] private GameObject upper2;
    [SerializeField] private GameObject upper3;
    [SerializeField] private GameObject upper4;
    [SerializeField] private GameObject bottom1;
    [SerializeField] private GameObject bottom2;
    [SerializeField] private GameObject bottom3;
    [SerializeField] private GameObject bottom4;

    [SerializeField] private float power = 10f;
    [SerializeField] private float radius = 5f;

    public Camera cam;

    private bool onetime = false;
    public static bool zooming = false;
    public static bool startShake = false;
    public static Vector3 pomePos { get; set; }


    private Coroutine shakeCrt = null;
    private int comboCount;

    public AudioSource sfx;

    public override void CutInHalf()
    {
        if (!onetime) 
        {
            onetime = true;
            zooming = true;
            CamZoom.pomePos = transform.position + new Vector3(0, 0f, -28f);
            StartCoroutine(SlowSpeed(2f));
            StartCoroutine(BackToNormal());

            if (shakeCrt != null)
            {
                StopCoroutine(shakeCrt);
                startShake = false;
            }

            shakeCrt = StartCoroutine(StartShake());
        }
        if (!GameManager.instance.endGameState)
            GameManager.instance.ScoreIncrease();
    }
    public override void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, transform.position, radius, 0f, ForceMode.Force);
            }
            
        }
    }

    IEnumerator SlowSpeed(float duration)
    {
        speed = Mathf.Lerp(defaultSpeed, 0.05f, 1f);
        float count = 0;
        while (count < duration)
        {
            count += Time.fixedDeltaTime;
            foreach (Fruits fruits in GameManager.instance.fruitsList)
            {
                fruits.speed = Mathf.Lerp(fruits.defaultSpeed, 0.1f, 1f);
                if (fruits.rb != null)
                {
                    fruits.rb.angularVelocity -= new Vector3(0.001f, 0.001f, 0.001f);
                }
                fruits.isSlowed = true;
            }
            yield return null;
        }
    }

    public override void OnMouseEnter()
    {
        comboCount++;
        if (comboCount >= 3)
        {
            GameManager.instance.TextDisPlay(comboCount.ToString() + " hits", transform.position);
        }
        if(startShake == true)
        {
           CamShake.Shake(0.4f, 0.4f);
        }
        GameManager.instance.mainSpawner.gameObject.SetActive(false);
        sfx.clip = GameManager.instance.otherClip[3];
        sfx.Play();
        CutInHalf();
    }

    IEnumerator StartShake()
    {
        yield return new WaitForSeconds(0.8f);
        startShake = true;
    }

    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(4f);
        var u1 = Instantiate(upper1, transform.position, transform.rotation);
        var u2 = Instantiate(upper2, transform.position, transform.rotation);
        var u3 = Instantiate(upper3, transform.position, transform.rotation);
        var u4 = Instantiate(upper4, transform.position, transform.rotation);
        var u5 = Instantiate(bottom1, transform.position, transform.rotation);
        var u6 = Instantiate(bottom2, transform.position, transform.rotation);
        var u7 = Instantiate(bottom3, transform.position, transform.rotation);
        var u8 = Instantiate(bottom4, transform.position, transform.rotation);
        Destroy(u1, 2f);
        Destroy(u2, 2f);
        Destroy(u3, 2f);
        Destroy(u4, 2f);
        Destroy(u5, 2f);
        Destroy(u6, 2f);
        Destroy(u7, 2f);
        Destroy(u8, 2f);
        foreach (Fruits fruits in GameManager.instance.fruitsList)
        {
            fruits.speed = GameManager.instance.speed;
            if (fruits.rb != null)
            {
                fruits.rb.angularVelocity -= new Vector3(0.05f, 0.05f, 0.05f);
            }
            fruits.isSlowed = false;
        }
        startShake = false;
        zooming = false;

        GameManager.instance.score += comboCount - 1;
        GameManager.instance.ScoreIncrease();
        comboCount = 0;

        GameManager.instance.speed += GameManager.instance.acceleration;
        speed = GameManager.instance.speed;
        GameManager.instance.mainSpawner.gameObject.SetActive(true);
        base.CutInHalf();
    }
}
