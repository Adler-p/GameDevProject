using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dect : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey(KeyCode.S))
        {
            transform.parent.GetComponent<Ladder>().down(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            transform.parent.GetComponent<Ladder>().down(collision);
        }
    }
}
