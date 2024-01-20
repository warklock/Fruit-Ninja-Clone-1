using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using VFX;

public class Fruits : MonoBehaviour
{
    public string _name;
    public Rigidbody rb;
    public float speed;
    public float defaultSpeed;

    public float horizontal;
    public float vertical;
    public float verticalRange;

    public bool isfalling = false;
    public bool isSlowed = false;

    public static bool startCounting = false;

    public GameObject combo_Text;
    public GameObject old_comboText;

    [SerializeField] private GameObject upperHalf;
    [SerializeField] private GameObject bottomHalf;
    [SerializeField] private GameObject splash;


    void Start()
    {
        /*rb  = GetComponent<Rigidbody>();*/
        /*horizontal = UnityEngine.Random.Range(-30, 30);*/
        /*vertical = 0;*/
        /*verticalRange = UnityEngine.Random.Range(80, 100);*/
        isfalling = false;
        speed = GameManager.instance.speed;
        defaultSpeed = GameManager.instance.speed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null)
        {
            Isfaliing();
            if (!isSlowed)
            {
                Vertical();
            }
            rb.velocity = new Vector3(horizontal, vertical, 0) * speed * Time.fixedDeltaTime;
        }
    }

    public void Vertical()
    {
        if (isfalling == true)
        {
            vertical -= Mathf.Lerp(0, speed, 0.25f);
        }
    }

    private void Isfaliing()
    {
        if (transform.position.y >= 16f)
        {
            isfalling = true;
        }
    }

    public virtual void CutInHalf()
    {
        GameManager.instance.fruitsList.Remove(this);

        if (!GameManager.instance.endGameState)
            GameManager.instance.ScoreIncrease();

        GameManager.instance.PomegraneteSpawn();

        if (upperHalf != null)
            Instantiate(upperHalf, transform.position, transform.rotation);

        if (bottomHalf != null)
            Instantiate(bottomHalf, transform.position, transform.rotation);

        if (splash != null)
            Instantiate(splash, transform.position + new Vector3(0, 0, 0), transform.rotation);
        
        Explosion();
        Destroy(gameObject);
    }

    public virtual void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(300, transform.position, 1000, 6f, ForceMode.Force);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            Physics.IgnoreCollision(GetComponent<SphereCollider>(), collision.gameObject.GetComponent<SphereCollider>());
        }
    }

    public virtual void OnMouseEnter()
    {
        GameManager.instance.startComboCounting = true;
        GameManager.instance.comboCount++;
        GameManager.instance.fruitEndOfComboPosition = transform.position;
        GameManager.instance.ComboCount();
        CutInHalf();
    }
}
