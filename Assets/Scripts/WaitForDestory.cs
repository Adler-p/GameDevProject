using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDestory : MonoBehaviour
{
    public float canStandTime = 2f;
    public float DestoryTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(destroyAndCreate());
    }

    IEnumerator destroyAndCreate()
    {
        yield return new WaitForSeconds(canStandTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(DestoryTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
