using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public Sprite Sprites;
    public GameObject vfx;
    [SerializeField]private bool isChange = false;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        change(collision.gameObject,false);
        //isChange = true;
    }

    public void change(GameObject collision, bool isTriger)
    {
        if (collision.transform.tag == "Player" && (isTriger ||  collision.transform.position.y > transform.position.y) && !isChange)
        {
            //SoundControl.instance.playSound("iceGround");
            GM.instance.addFreeze(1);
            GetComponent<SpriteRenderer>().sprite = Sprites;
            Instantiate(vfx, transform.position, Quaternion.identity);
            isChange = true;
            GetComponent<AudioSource>().Play();
        }
        else if (collision.transform.tag == "Player2" && (isTriger || collision.transform.position.y > transform.position.y) && !isChange)
        {
            //SoundControl.instance.playSound("iceGround");
            GM.instance.addFreeze(2);
            GetComponent<SpriteRenderer>().sprite = Sprites;
            Instantiate(vfx, transform.position, Quaternion.identity);
            isChange = true;
            GetComponent<AudioSource>().Play();
        }
        else if (collision.transform.tag == "Ice" && (isTriger || collision.transform.position.y > transform.position.y) && !isChange)
        {
            if (collision.transform.GetComponent<FreezeControl>().hitterName.Equals("Player2"))
            {
                GM.instance.addFreeze(2);
            }
            else
            {
                GM.instance.addFreeze(1);
            }
            //SoundControl.instance.playSound("iceGround");
            GetComponent<SpriteRenderer>().sprite = Sprites;
            Instantiate(vfx, transform.position, Quaternion.identity);
            isChange = true;
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter");
        change(collision.gameObject,true);
        //isChange = true;
    }
}
