using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour
{
    public List<GameObject> Lev1Star;
    public List<GameObject> Lev2Star;
    public List<GameObject> Lev3Star;
    public List<GameObject> Lev4Star;
    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            Lev1Star[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            Lev2Star[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            Lev3Star[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            Lev4Star[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 1;
        for (int i = 0; i < Data.instance.starNumInLev1; i++)
        {
            Lev1Star[i].SetActive(true);
        }
        for (int i = 0; i < Data.instance.starNumInLev2; i++)
        {
            Lev2Star[i].SetActive(true);
        }
        for (int i = 0; i < Data.instance.starNumInLev3; i++)
        {
            Lev3Star[i].SetActive(true);
        }
        for (int i = 0; i < Data.instance.starNumInLev4; i++)
        {
           Lev4Star[i].SetActive(true);
        }
    }
}
