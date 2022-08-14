using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public int lev;
    public static GM instance;        
    public List<GameObject> healthUI;   //HP count
    public List<GameObject> healthUI2;  //HP count
    public List<GameObject> star;
    public GameObject coinText;         //coin score text
    public GameObject Player1FreezeText;    //percentage frozen p1
    public GameObject Player2FreezeText;    //percentage frozen p1

    public GameObject player2;             
    public GameObject player2Info;         

    public Scrollbar player1FreezeSlider;   //percentage frozen slidding bar by p1
    public Scrollbar player2FreezeSlider;   //percentage frozen slidding bar by p2

    public Transform pos;                   //starting pos

    public float finishTime = 10f;

    private float currentTime;

    public GameObject mainPanel;            
    public GameObject failPanel;            
    public GameObject successPanel;
    public GameObject backGroundMusic;

    public Text time;

    private int needCoinNum;            
    private int coinNum;                
    private int needFreezeNum;          
    [SerializeReference]private int currentPlayer1FreezeNum;    
    [SerializeReference]private int currentPlayer2FreezeNum;

    bool isReadyFinishedMusic = false;


    private enum EndState
    {
        Default,
        ReluctantlyWin,
        Win,
        CompleteWin
    }

    private EndState endState;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        instance = this;
        coinNum = 0;
        currentTime = 0;
        needFreezeNum = GameObject.FindGameObjectsWithTag("canFreezeItem").Length;  
        needCoinNum = GameObject.FindGameObjectsWithTag("coin").Length;
        isReadyFinishedMusic = false;

        if (Data.instance.PlayerNum == 2)         
        {
            Instantiate(player2,pos.position,Quaternion.identity);
            player2Info.SetActive(true);
        }   
        else
        {
            player2Info.SetActive(false);
        }

        for (int i = 0; i < 3; i++)
        {
            star[i].SetActive(false);
        }

        //print(needFreezeNum);

    }

    // Update is called once per frame
    void Update()
    {
        time.text = ((int)(finishTime - currentTime)).ToString() + "s";
        if(currentTime < finishTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            time.enabled = false;
        }

        print(needFreezeNum);
        
        coinText.GetComponent<Text>().text = coinNum.ToString();

        Player1FreezeText.GetComponent<Text>().text = ((currentPlayer1FreezeNum * 100 / needFreezeNum)).ToString();
        player1FreezeSlider.value = (float)currentPlayer1FreezeNum / (float)needFreezeNum;

        Player2FreezeText.GetComponent<Text>().text = ((currentPlayer2FreezeNum * 100 / needFreezeNum)).ToString();
        player2FreezeSlider.value = (float)currentPlayer2FreezeNum / (float)needFreezeNum;
        
        if((Data.instance.PlayerNum == 2))
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().playerHealth <= 0 && GameObject.FindGameObjectWithTag("Player2").GetComponent<CharacterController2D>().playerHealth <= 0)
            {
                failPanel.SetActive(true);
                Time.timeScale = 0;
                if (isReadyFinishedMusic == false)
                {
                    isReadyFinishedMusic = true;
                    SoundControl.instance.playSound("Fail");
                    backGroundMusic.GetComponent<AudioSource>().Stop();
                }
                mainPanel.SetActive(false);
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().playerHealth <= 0)
            {
                failPanel.SetActive(true);
                Time.timeScale = 0;
                if (isReadyFinishedMusic == false)
                {
                    isReadyFinishedMusic = true;
                    SoundControl.instance.playSound("Fail");
                    backGroundMusic.GetComponent<AudioSource>().Stop();
                }
                mainPanel.SetActive(false);
            }
        }

        if(currentPlayer1FreezeNum + currentPlayer2FreezeNum == needFreezeNum)
        {
            successPanel.SetActive(true);
            Time.timeScale = 0;
            
            if (isReadyFinishedMusic == false)
            {
                isReadyFinishedMusic = true;
                SoundControl.instance.playSound("Success");
                backGroundMusic.GetComponent<AudioSource>().Stop();
            }
            mainPanel.SetActive(false);

            endState = EndState.ReluctantlyWin;

            if (coinNum == needCoinNum)
            {
                endState = EndState.Win;

                if(currentTime < finishTime)
                {
                    endState = EndState.CompleteWin;
                }
            }
            if(endState == EndState.ReluctantlyWin)
                Data.instance.setLev(1,lev);
            else if (endState == EndState.Win)
            {
                Data.instance.setLev(2, lev);
            }
            else if (endState == EndState.CompleteWin)
            {
                Data.instance.setLev(3, lev);
            }

        }

        switch (endState)
        {
            case EndState.ReluctantlyWin:
                for(int i = 0; i < 1; i++)
                {
                    star[i].SetActive(true);
                }
                break;
            case EndState.Win:
                for (int i = 0; i < 2; i++)
                {
                    star[i].SetActive(true);
                }
                break;
            case EndState.CompleteWin:
                for (int i = 0; i < 3; i++)
                {
                    star[i].SetActive(true);
                }
                break;
        }

    }
    public void deleteHealth(int playerHealth,int lev)
    {
        if (lev == 1)
            healthUI[playerHealth - 1].SetActive(false);
        else
            healthUI2[playerHealth - 1].SetActive(false);
    }

    public void addHealth(int playerHealth, int lev)
    {
        print(playerHealth + "     k      " +lev);
        if (lev == 1)
            healthUI[playerHealth - 1].SetActive(true);
        else
            healthUI2[playerHealth - 1].SetActive(true);

    }
    public bool isOpen = true;
    [SerializeField]private Sprite lastSprite;
    public Sprite sprite;
    public void CloseOrOpen(Image image)
    {
        if (isOpen)
        { 
            lastSprite = image.sprite;
            image.sprite = sprite;
            isOpen = false;
        }
        else{
            image.sprite = lastSprite;
            isOpen = true;
        }
    }
    
    public void addCoin()
    {
        coinNum += 1;
    }
    public void addFreeze(int lev)
    {
        if(lev == 1)
            currentPlayer1FreezeNum += 1;
        else
            currentPlayer2FreezeNum += 1;
    }
}
