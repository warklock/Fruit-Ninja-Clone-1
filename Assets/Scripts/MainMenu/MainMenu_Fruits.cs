using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu_Fruits : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [SerializeField] private GameObject upperHalf;
    [SerializeField] private GameObject bottomHalf;
    [SerializeField] private GameObject splash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles = transform.eulerAngles - new Vector3 (x,y,z);
    }

    public virtual void CutInHalf()
    {
        if (upperHalf != null)
            Instantiate(upperHalf, transform.position, transform.rotation);

        if (bottomHalf != null)
            Instantiate(bottomHalf, transform.position, transform.rotation);

        if (splash != null)
            Instantiate(splash, transform.position + new Vector3(0, 0, 0), transform.rotation);

        gameObject.SetActive(false);
        Explosion();
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

    public void LoadScene(int i)
    {
        SceneManager.LoadSceneAsync(i);
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }
}
