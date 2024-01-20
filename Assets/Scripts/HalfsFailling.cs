using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HalfsFailling : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5.0f;
    private float h;
    private float z;
    private float y;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyFruit());
        h = UnityEngine.Random.Range(0, 1000f);
        z = UnityEngine.Random.Range(0, 1000f);
        y = 200f;
    }

    void FixedUpdate()
    {
        while (h > 0) { h--; }
        while (z > 0) { z--; }
        while (y > -15) { y--; }
        rb.AddForce(new Vector3(h,y,z));
    }

    private IEnumerator DestroyFruit()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
