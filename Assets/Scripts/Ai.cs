using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai : MonoBehaviour
{
    [Header("Settings")]
    public float walkSpeed;         
    public float offect = 1f;       
    public float freezeTime = 10f;  
    public float rayLen = 3f;       

    [Header("Body Obj")]            
    public GameObject Body;
    public GameObject Head;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject rightArm; 
    public GameObject leftArm;

    [Header("Perfabs")]
    public GameObject ice;         

    [Header("Components")]
    private Animator anim;

    public string hitterName;

    public enum State              
    {
        IDEL,
        WALK,
        FREEZE,
        FREEZED
    }
    public State currentState;     
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentState = State.WALK;     
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.FREEZE)           
        {
            SoundControl.instance.playSound("icedEnemySound");
            GameObject obj = Instantiate(ice,transform.position + transform.up*0.95f,Quaternion.identity);   
            currentState = State.FREEZED;
            obj.GetComponent<FreezeControl>().hitterName = hitterName;
            StartCoroutine(waitForUnfreeze());                          
        }
        else if(currentState == State.FREEZED)  
        {   
            anim.speed = 0;    
        }
        else if(currentState == State.WALK)
        {
            anim.speed = 1;     
            if (needTurnToBack())       
            {
                transform.right = -transform.right;
                //restLayer();
            }
            transform.position += transform.right * walkSpeed * Time.deltaTime; 
            anim.SetBool("Walk", true);     
        }
        else if(currentState == State.IDEL)
        {
            anim.SetBool("Walk", false);
        }
    }

    private void restLayer()
    {
        Body.GetComponent<SpriteRenderer>().sortingOrder = -Body.GetComponent<SpriteRenderer>().sortingOrder;
        Head.GetComponent<SpriteRenderer>().sortingOrder = -Head.GetComponent<SpriteRenderer>().sortingOrder;
        leftArm.GetComponent<SpriteRenderer>().sortingOrder = -leftArm.GetComponent<SpriteRenderer>().sortingOrder;
        rightArm.GetComponent<SpriteRenderer>().sortingOrder = -rightArm.GetComponent<SpriteRenderer>().sortingOrder;
        leftLeg.GetComponent<SpriteRenderer>().sortingOrder = -leftLeg.GetComponent<SpriteRenderer>().sortingOrder;
        rightLeg.GetComponent<SpriteRenderer>().sortingOrder = -rightLeg.GetComponent<SpriteRenderer>().sortingOrder;
        leftHand.GetComponent<SpriteRenderer>().sortingOrder = -leftHand.GetComponent<SpriteRenderer>().sortingOrder;
        rightHand.GetComponentInParent<SpriteRenderer>().sortingOrder = -rightHand.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private bool needTurnToBack()
    {
        Ray2D ray = new Ray2D(transform.position + transform.right*offect,-transform.up); 
        //RaycastHit2D hit;
        Debug.DrawRay(ray.origin,ray.direction,Color.red);
        if (Physics2D.Raycast(ray.origin, ray.direction, rayLen))      
        {
            return false;
        }
        else                                                     
        {
            print("uuu");
            return true;
        }

    }

    public void freeze(string tag)
    {
        currentState = State.FREEZE;
        hitterName = tag;
    }

    IEnumerator waitForUnfreeze()
    {
        yield return new WaitForSeconds(freezeTime);        
        currentState = State.WALK;                          
    }

}
