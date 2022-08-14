using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public float dieTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(dieTime);
        Destroy(gameObject);
    }
}
