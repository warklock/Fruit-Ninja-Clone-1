using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnOff : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("Boom on");
    }

    private void OnEnable()
    {
        Debug.Log("Boom on");
    }

    private void OnDisable()
    {
        Debug.Log("Boom off");
    }
}
