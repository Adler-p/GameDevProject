using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PowerUp : Item
{
    public override void GetQuality(GameObject obj)
    {
        ItemControl.instance.StartPowerUp(obj);
    }
}
