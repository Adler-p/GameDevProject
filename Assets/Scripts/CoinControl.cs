using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //coin controll
    {
        if(collision.tag == "Player" || collision.tag == "Player2")
        {
            GM.instance.addCoin();
            
            Destroy(gameObject);
            SoundControl.instance.playSound("Coin");
        }
    }
}
