using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Inviciblity : Item
{
    public override void GetQuality(GameObject obj)
    {
        ItemControl.instance.startInvincible(obj);
    }
}
