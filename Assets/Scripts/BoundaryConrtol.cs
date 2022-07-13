using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryConrtol : MonoBehaviour
{
                                                        
    private void OnTriggerEnter2D(Collider2D collision)         
    {
        if(collision.tag == "Player" || collision.tag == "Player2")        
        {
            collision.GetComponent<CharacterController2D>().death();
        }
    }
}
