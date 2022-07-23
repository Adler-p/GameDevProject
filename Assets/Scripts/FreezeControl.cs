using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeControl : MonoBehaviour
{
    public string hitterName;
    public float deathTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(deathCount());
    }

    IEnumerator deathCount()    
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

}
