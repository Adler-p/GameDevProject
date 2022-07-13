using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public float moveTime = 2f;     
    public float moveSpeed = 2f;    

    public float maxHeight;         
    public float lowHeight;         
    public void startMove()
    {
        StartCoroutine(move());     
    }
    public void stopMove()
    {
        StopAllCoroutines();
    }

    IEnumerator move()
    {
        while (true)
        {
            float currentTime = 0;
            while (currentTime < moveTime && transform.localPosition.y < maxHeight) 
            {
                yield return new WaitForSeconds(0.01f);
                currentTime += 0.01f;
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }

            currentTime = 0;
            while (currentTime < moveTime && transform.localPosition.y > lowHeight)
            {
                yield return new WaitForSeconds(0.01f);
                currentTime += 0.01f;
                transform.position += -Vector3.up * moveSpeed * Time.deltaTime;
            }
        }
    }
}
