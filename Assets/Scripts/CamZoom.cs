using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VFX;

public class CamZoom : MonoBehaviour
{
    public float zoom = 55f;
    public float zoomMultiplier = 4f;
    public float zoomMin = 11f;
    public float zoomMax = 55f;
    public float velocity = 6f;
    public float smoothTime = 0.25f;

    [SerializeField] private Camera cam;
    public Vector3 pos1;
    public static Vector3 pomePos { get; set; }
    public Vector3 zoomOutVelocity = Vector3.zero;
    public Vector3 angleVelocity = Vector3.zero;

    void Start()
    {
        zoom = cam.fieldOfView;
        pos1 = cam.transform.position;
        pomePos = cam.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Pomegranate.startShake == false)
        {
            PomeZoom();
        }
    }

    public void PomeZoom()
    {
        if (Pomegranate.zooming == true)
        {
            zoom -= zoomMultiplier;
            zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, zoom, ref velocity, smoothTime);
            cam.transform.position = Vector3.SmoothDamp(transform.position, pomePos, ref zoomOutVelocity, smoothTime);
            cam.transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, new Vector3(0,0,35), ref angleVelocity, smoothTime);
        }
        else
        {
            zoom += zoomMultiplier;
            zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, zoom, ref velocity, smoothTime);
            cam.transform.position = Vector3.SmoothDamp(transform.position, pos1, ref zoomOutVelocity, smoothTime);
            cam.transform.eulerAngles = new Vector3(0,0,0);
        }

    }
}
