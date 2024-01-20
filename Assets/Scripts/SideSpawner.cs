using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawner : Spawner
{
    public bool on = false;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private float X;

    private float count;
    [SerializeField] private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        count = 0f;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        count += Time.fixedDeltaTime;
        foreach (Fruits fruit in GameManager.instance.fruitsList)
        {
            if (fruit != null)
                fruit.gameObject.SetActive(true);
        }
        if (GameManager.instance.endGameState == false && on == true && count >= cooldown && Pomegranate.zooming == false)
        {
            count = 0f;
            SpawnNormalFruits(new Vector3 (transform.position.x + X,transform.position.y,transform.position.z), UnityEngine.Random.Range(xMin,xMax), UnityEngine.Random.Range(yMin, yMax));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit")
        {
            GameManager.instance.fruitsList.Remove(other.GetComponent<Fruits>());
            if (GameManager.instance.endGameState == false)
            {
                GameManager.instance.GameProcess();
            }
            Destroy(other.gameObject);
        }
    }
}
