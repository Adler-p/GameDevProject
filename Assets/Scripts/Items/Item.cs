using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Player2")
        {
            GetQuality(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public abstract void GetQuality(GameObject obj);
}
