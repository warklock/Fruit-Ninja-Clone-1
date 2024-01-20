using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFX;

public class SpecialBanana : Fruits
{
    public override void OnMouseEnter()
    {
        GameManager.instance.Freeze();
       base.OnMouseEnter();
    }
}
