using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUpForce : MonoBehaviour
{
    // This script is for the jumping tile to jump

    public float force = 20f;
    private void Start()

    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detect collision with player 
        
        if(collision.transform.tag == "Player" || collision.transform.tag == "Player2")
        {
            collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * force;
        }
    }
}
