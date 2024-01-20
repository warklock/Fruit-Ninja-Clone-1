using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWatermelon : Fruits
{
    public override void OnMouseEnter()
    {
        GameManager.instance.TimeIncrease();
        GameManager.instance.TextDisPlay("+ " + GameManager.instance.timeBonusMult + " s", transform.position);
        base.OnMouseEnter();
    }
}
