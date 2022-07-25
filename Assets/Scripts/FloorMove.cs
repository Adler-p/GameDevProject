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
    private bool isMove = false;
    public void startMove()
    {
        if (isMove == true)
            return;
        isMove = true;
        StartCoroutine(move());
    }
    public void stopMove()
    {
        if (isMove == true)
            StopAllCoroutines();
        isMove = false;
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
        print(isMove);
        while (isMove)
        {
            print((transform.localPosition.y < maxHeight || !isVertial));
            print((isVertial || transform.localPosition.x < rightBoundary));
            float currentTime = 0;
            while (currentTime < moveTime && (transform.localPosition.y < maxHeight || !isVertial) && (isVertial || transform.localPosition.x < rightBoundary))
            {
                yield return new WaitForSeconds(0.01f);
                currentTime += 0.01f;
                transform.position += origin * moveSpeed * Time.deltaTime;
            }
            print("jin");
            currentTime = 0;
            while (currentTime < moveTime && (transform.localPosition.y > lowHeight || !isVertial) && (isVertial || transform.localPosition.x > leftBoundary))
            {
                yield return new WaitForSeconds(0.01f);
                currentTime += 0.01f;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }
}
