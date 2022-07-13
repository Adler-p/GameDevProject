using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public Sprite Sprites;
    public GameObject vfx;
    private bool isChange = false;
    //玩家1或2碰撞改变方块地形
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && collision.transform.position.y > transform.position.y && !isChange)
        {
            GM.instance.addFreeze(1);
            GetComponent<SpriteRenderer>().sprite = Sprites;
            Instantiate(vfx,transform.position,Quaternion.identity);
            isChange = true;
        }
        else if(collision.transform.tag == "Player2" && collision.transform.position.y > transform.position.y && !isChange)
        {
            GM.instance.addFreeze(2);
            GetComponent<SpriteRenderer>().sprite = Sprites;
            Instantiate(vfx, transform.position, Quaternion.identity);
            isChange = true;
        }
    }
}
