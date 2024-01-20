using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Fruits
{
    public GameObject explosion;
    public override void CutInHalf()
    {
        GameManager.instance.sfx.clip = GameManager.instance.otherClip[5];
        GameManager.instance.sfx.Play();
        GameObject explo = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explo, 1f);
        if (GameManager.instance.endGameState == false)
        GameManager.instance.GameProcess();
        base.CutInHalf();
    }
}

