using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public static GM instance;        
    public List<GameObject> healthUI;   //HP count
    public List<GameObject> healthUI2;  //HP count
    public GameObject coinText;         //coin score text
    public GameObject Player1FreezeText;    //percentage frozen p1
    public GameObject Player2FreezeText;    //percentage frozen p1

    public GameObject player2;             
    public GameObject player2Info;         

    public Scrollbar player1FreezeSlider;   //percentage frozen slidding bar by p1
    public Scrollbar player2FreezeSlider;   //percentage frozen slidding bar by p2

    public Transform pos;                   //starting pos

    public GameObject mainPanel;            
    public GameObject failPanel;            
    public GameObject successPanel;         

    private int needCoinNum;            
    private int coinNum;                
    private int needFreezeNum;          
    [SerializeReference]private int currentPlayer1FreezeNum;    
    [SerializeReference]private int currentPlayer2FreezeNum;    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coinNum = 0;
        needFreezeNum = GameObject.FindGameObjectsWithTag("canFreezeItem").Length;  
        needCoinNum = GameObject.FindGameObjectsWithTag("coin").Length;             
        
        if(GameObject.Find("GameData").GetComponent<Data>().PlayerNum == 2)         
        {
            Instantiate(player2,pos.position,Quaternion.identity);
            player2Info.SetActive(true);
        }   
        else
        {
            player2Info.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        print(needFreezeNum);
        
        coinText.GetComponent<Text>().text = coinNum.ToString();

        Player1FreezeText.GetComponent<Text>().text = ((currentPlayer1FreezeNum * 100 / needFreezeNum)).ToString();
        player1FreezeSlider.value = (float)currentPlayer1FreezeNum / (float)needFreezeNum;

        Player2FreezeText.GetComponent<Text>().text = ((currentPlayer2FreezeNum * 100 / needFreezeNum)).ToString();
        player2FreezeSlider.value = (float)currentPlayer2FreezeNum / (float)needFreezeNum;
        
        if(GameObject.Find("GameData").GetComponent<Data>().PlayerNum == 2)
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().playerHealth <= 0 && GameObject.FindGameObjectWithTag("Player2").GetComponent<CharacterController2D>().playerHealth <= 0)
            {
                failPanel.SetActive(true);
                mainPanel.SetActive(false);
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().playerHealth <= 0)
            {
                failPanel.SetActive(true);
                mainPanel.SetActive(false);
            }
        }

        if(currentPlayer1FreezeNum + currentPlayer2FreezeNum == needFreezeNum && coinNum == needCoinNum)
        {
            successPanel.SetActive(true);
            mainPanel.SetActive(false);
        }

    }
    public void deleteHealth(int playerHealth,int lev)
    {
        if (lev == 1)
            Destroy(healthUI[playerHealth-1]);
        else
            Destroy(healthUI2[playerHealth - 1]);
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
