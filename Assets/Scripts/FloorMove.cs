using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public float moveTime = 2f;     
    public float moveSpeed = 2f;
    public bool isVertial;
    public float maxHeight;         
    public float rightBoundary;         
    public float lowHeight;
    public float leftBoundary;

    private Vector3 origin;
    private Vector3 direction;
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
        if (isVertial)
        {
            origin = Vector3.up;
            direction = Vector3.down;
        }
        else
        {
            origin = Vector3.right;
            direction = Vector3.left;
        }
        while (true)
        {
            float currentTime = 0;
            while (currentTime < moveTime && transform.localPosition.y < maxHeight && (isVertial || transform.localPosition.x < rightBoundary))
            {
                yield return new WaitForSeconds(0.01f);
                currentTime += 0.01f;
                transform.position += origin * moveSpeed * Time.deltaTime;
            }

            currentTime = 0;
            while (currentTime < moveTime && transform.localPosition.y > lowHeight && (isVertial || transform.localPosition.x > leftBoundary))
            {
                yield return new WaitForSeconds(0.01f);
                currentTime += 0.01f;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }
}
