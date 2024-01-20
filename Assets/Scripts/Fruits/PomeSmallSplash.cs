using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PomeSmallSplash : MonoBehaviour
{
    [SerializeField] GameObject spallSplash;
    public float duration;


    private float countTime = 0;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        countTime += Time.deltaTime;
        if (countTime >= duration)
        {
            Instantiate(spallSplash, transform.position + new Vector3(0, 0, -1), transform.rotation);
            countTime = 0;
        }
        
    }
}
