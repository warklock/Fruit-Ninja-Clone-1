using System;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float countTime = 0f;

    public int pome_Cooldown;

    private void Start()
    {
        pome_Cooldown = GameManager.instance.pome_turn_cooldown;
    }

    public virtual void FixedUpdate()
    {
        SpawnCooldown();
        foreach (Fruits fruit in GameManager.instance.fruitsList)
        {
            if (fruit != null)
                fruit.gameObject.SetActive(true);
        }

        if (countTime >= GameManager.instance.spawnCooldown && GameManager.instance.fruitsList.Count <= GameManager.instance.MaximumFruitPerTurn && GameManager.instance.endGameState == false)
        {
            spawnBomb();
            spawnSpecialBanana();
            spawnSpecialWaterMelon();
            SpawnNormalFruits( new Vector3 (UnityEngine.Random.Range(-8, 8), 1, -9), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(45, 55));
        }
    }

    public virtual Fruits SpawnNormalFruits(Vector3 vt3 , float horizontal, float vertical)
    {
        int random = UnityEngine.Random.Range(0, GameManager.instance.initialFruitsList.Count);
        Fruits fruit = Instantiate(GameManager.instance.initialFruitsList[random], vt3, transform.rotation);
        fruit.horizontal = horizontal;
        fruit.vertical = vertical;
        GameManager.instance.fruitsList.Add(fruit);
        return fruit;
    }

    public void spawnPome()
    {
        Fruits pomeObj = Instantiate(GameManager.instance.pomegranete, new Vector3(UnityEngine.Random.Range(-8, 8), 1, -9), transform.rotation);
        GameManager.instance.fruitsList.Add(pomeObj);
        pomeObj.gameObject.SetActive(true);
        pomeObj.horizontal = UnityEngine.Random.Range(-10, 10);
        pomeObj.vertical = UnityEngine.Random.Range(45, 55);
        if (GameManager.instance.spawnCooldown >= 0.8f)
        {
            GameManager.instance.spawnCooldown -= GameManager.instance.spawnCooldownDeduction;
        }
    }

    public void spawnBomb()
    {
        float chance = UnityEngine.Random.Range(0, 100f);
        if (chance <= GameManager.instance.bombSpawnChance)
        {
            Fruits boom = Instantiate(GameManager.instance.bomb, new Vector3(UnityEngine.Random.Range(-8, 8), 1, -9), transform.rotation);
            boom.gameObject.SetActive(true);
            GameManager.instance.fruitsList.Add(boom);
            boom.horizontal = UnityEngine.Random.Range(-10, 10);
            boom.vertical = UnityEngine.Random.Range(45, 55);
        }
    }

    public void spawnSpecialBanana()
    {
        float chance = UnityEngine.Random.Range(0, 100f);
        if (chance <= GameManager.instance.specialSpawnChance)
        {
            Fruits boom = Instantiate(GameManager.instance.specialBanana, new Vector3(UnityEngine.Random.Range(-8, 8), 1, -9), transform.rotation);
            boom.gameObject.SetActive(true);
            GameManager.instance.fruitsList.Add(boom);
            boom.horizontal = UnityEngine.Random.Range(-10, 10);
            boom.vertical = UnityEngine.Random.Range(45, 55);
        }
    }

    public void spawnSpecialWaterMelon()
    {
        float chance = UnityEngine.Random.Range(0, 100f);
        if (chance <= GameManager.instance.specialSpawnChanceWatermelon)
        {
            Fruits boom = Instantiate(GameManager.instance.specialWaterMelon, new Vector3(UnityEngine.Random.Range(-8, 8), 1, -9), transform.rotation);
            boom.gameObject.SetActive(true);
            GameManager.instance.fruitsList.Add(boom);
            boom.horizontal = UnityEngine.Random.Range(-10, 10);
            boom.vertical = UnityEngine.Random.Range(45, 55);
        }
    }

    public void SpawnCooldown()
    {
        countTime += Time.fixedDeltaTime;
        if (countTime >= GameManager.instance.spawnCooldown + 0.2f && GameManager.instance.endGameState == false)
        {
            countTime = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fruit_Parts")
        {
            GameManager.instance.fruitsList.Remove(other.GetComponent<Fruits>());
            Destroy(other.gameObject);
        }
        else
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
