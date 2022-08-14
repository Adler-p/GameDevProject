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

    public AudioSource backGroundSound;
    public List<AudioSource> GroundSound;
    public AudioSource otherSound;

    private bool backSoundTurn;
    private bool groundSoundTurn; 
    private bool otherSoundTurn;

    // Start is called before the first frame update
    void Start()
    {
        backSoundTurn = true;
        groundSoundTurn = true;
        otherSoundTurn = true;
        instance = this;
        audioSource = GetComponent<AudioSource>();
        if(isBackGroundSound)
        {
            audioSource.Play();
            StartCoroutine(playBackGroundClip());
        }
        GameObject[] objs = GameObject.FindGameObjectsWithTag("canFreezeItem");
        for(int i = 0; i < objs.Length; i++)
        {
            GroundSound.Add(objs[i].GetComponent<AudioSource>());
        }
    }
    
    public void playSound(string name)
    {
        if (name.Equals("Jump"))
        {
            audioSource.clip = allClip[0];
            audioSource.time = 0f;
            audioSource.Play();
        }

        else if (name.Equals("Coin"))
        {
            audioSource.clip = allClip[1];
            audioSource.time = 0f;
            audioSource.Play();
        }

        else if (name.Equals("Bounce"))
        {
            audioSource.clip = allClip[2];
            audioSource.time = 0f;
            audioSource.Play();
        }

        else if (name.Equals("iced"))
        {
            audioSource.clip = allClip[3];
            audioSource.time = 0f;
            audioSource.Play();
        }
        else if (name.Equals("MoveFloor"))
        {
            audioSource.clip = allClip[4];
            audioSource.time = 0f;
            audioSource.Play();
        }
        else if (name.Equals("hurt"))
        {
            audioSource.clip = allClip[5];
            audioSource.time = 0f;
            audioSource.Play();
        }
        else if (name.Equals("icedEnemySound"))
        {
            audioSource.clip = allClip[6];
            
            audioSource.Play();
        }
        else if (name.Equals("iceGround"))
        {
            audioSource.clip = allClip[7];
            audioSource.time = 0f;
            audioSource.Play();
        }
        
        else if (name.Equals("Fail"))
        {
            audioSource.clip = allClip[8];
            audioSource.time = 0f;
            audioSource.Play();
        }
        else if (name.Equals("Success"))
        {
            audioSource.clip = allClip[9];
            audioSource.time = 0f;
            audioSource.Play();
        }
        else if (name.Equals("UIClick"))
        {
            //print("enter");
            audioSource.clip = allClip[10];
            audioSource.time = 0f;
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

    public void closeSound(int type)
    {
        switch (type)
        {
            case 1:
                backSoundTurn = !backSoundTurn;
                if (backSoundTurn)
                {
                    backGroundSound.GetComponent<AudioSource>().volume = 1;
                }
                else
                {
                    backGroundSound.GetComponent<AudioSource>().volume = 0;
                }
                break;
            case 2:
                groundSoundTurn = !groundSoundTurn;
                if (groundSoundTurn)
                {
                    for(int i = 0; i < GroundSound.Count; i++)
                    {
                        GroundSound[i].volume = 1;
                    }
                }
                else
                {
                    for (int i = 0; i < GroundSound.Count; i++)
                    {
                        GroundSound[i].volume = 0;
                    }
                }
                break;
            case 3:
                otherSoundTurn = !otherSoundTurn;
                if (otherSoundTurn)
                {
                    otherSound.GetComponent<AudioSource>().volume = 1;
                }
                else
                {
                    otherSound.GetComponent<AudioSource>().volume = 0;
                }
                break;
        }
    }
}