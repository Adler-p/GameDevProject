using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public List<AudioClip> allClip;
    public AudioClip backGroundClip;
    public bool isBackGroundSound;
    public static SoundControl instance;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        if(isBackGroundSound)
        {
            audioSource.Play();
            StartCoroutine(playBackGroundClip());
        }
    }
    
    public void playSound(string name)
    {
        if (name.Equals("Jump"))
        {
            audioSource.clip = allClip[0];
            audioSource.Play();
        }

        else if (name.Equals("Coin"))
        {
            audioSource.clip = allClip[1];
            audioSource.Play();
        }
    }

    IEnumerator playBackGroundClip(){
        yield return new WaitForSeconds(4);
        audioSource.Stop();
        audioSource.clip = backGroundClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}