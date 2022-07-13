using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    
    public GameObject moveObj;
    private Animator _animator; 
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" || collision.transform.tag == "Player2" || collision.transform.tag == "Box") 
        {
            _animator.SetBool("up",true);
            _animator.SetBool("down", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Player2" || collision.transform.tag == "Box")
        {
            _animator.SetBool("down", true);
            _animator.SetBool("up", false);
        }
    }

    public void PressDown()
    {
        moveObj.GetComponent<FloorMove>().startMove();      
    }

    public void ButtonUp()
    {
        moveObj.GetComponent<FloorMove>().stopMove(); 
    }
}
