using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_AddHealth : Item
{
    public override void GetQuality(GameObject obj)
    {
        ItemControl.instance.addHealth(obj);
    }
}
