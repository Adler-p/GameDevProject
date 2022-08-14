using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;           
    public float walkSpeed = 2f;            
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;       
    public float flashTime = 0.2f;          
    public int playerHealth = 3;     

    [SerializeField]private bool facingRight = true;
    [SerializeField] private bool isGrounded = false;   
    [SerializeField] private bool isRun;
    [SerializeField] private bool isInvincibility = false;
    [SerializeField] private bool isFall = false;


    public Camera mainCamera;
    float moveDirection = 0;

    private Animator anim;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth > 0)
        {
            if (transform.tag == "Player")      
            {
                //isRun = Input.GetKey(KeyCode.LeftShift);
                isRun = true;
                // Movement controls
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                {
                    moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
                }
                else
                {
                    if (isGrounded || r2d.velocity.magnitude < 0.01f)
                    {
                        moveDirection = 0;
                    }
                }
            }
            else if(transform.tag == "Player2")     
            {
                isRun = true;

                // Movement controls
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
                {
                    moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
                }
                else
                {
                    if (isGrounded || r2d.velocity.magnitude < 0.01f)
                    {
                        moveDirection = 0;
                    }
                }
            }


            // Change facing direction
            if (moveDirection != 0)
            {
                if (moveDirection > 0 && !facingRight)
                {
                    facingRight = true;
                    t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
                }
                if (moveDirection < 0 && facingRight)
                {
                    facingRight = false;
                    t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
                }
            }
            if (transform.tag == "Player")
            {
                // Jumping
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                    anim.SetTrigger("Jump");
                    SoundControl.instance.playSound("Jump");
                }
            }
            else if(transform.tag == "Player2")
            {
                // Jumping
                if (Input.GetKeyDown(KeyCode.Keypad2) && isGrounded)
                {
                    r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
                    anim.SetTrigger("Jump");
                    SoundControl.instance.playSound("Jump");
                }
            }

            // Camera follow
            if (mainCamera)
            {
                mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
            }
        }
        else
        {
            moveDirection = 0;
            anim.SetBool("Death",true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    void FixedUpdate()
    {
        /*
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.5f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);  //���ؼ��?
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }*/

        Ray2D ray = new Ray2D(transform.position,-transform.up);
        
        if (Physics2D.Raycast(ray.origin, ray.direction,1.5f,(LayerMask.GetMask("Ground") | LayerMask.GetMask("ice"))))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        anim.SetBool("IsGround",isGrounded);
        anim.SetBool("IsFall",isFall);
        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * (isRun?maxSpeed:walkSpeed), r2d.velocity.y);       
        anim.SetFloat("Speed",moveDirection * (isRun?2:1));         

        // Simple debug
        //Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        //Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 enemyToPlayer = transform.position - collision.transform.position;  
        float euler = Vector2.Angle(collision.transform.up, enemyToPlayer);          
        if (collision.gameObject.CompareTag("Enemy") && euler < 30 && euler > -30 && collision.gameObject.GetComponent<Ai>().currentState != Ai.State.FREEZED)   
        {
            SoundControl.instance.playSound("iced");

            collision.transform.GetComponent<Ai>().freeze(transform.tag);
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.up * 5f;
        }
        else if (collision.gameObject.CompareTag("Enemy") && !isInvincibility && playerHealth > 0 && collision.gameObject.GetComponent<Ai>().currentState != Ai.State.FREEZED)
        {
            hit();
            Debug.Log("Collided with Enemy!");
        }
    }

    public void hit()
    {
        GM.instance.deleteHealth(playerHealth, transform.tag == "Player" ? 1 : 2);       
        playerHealth -= 1;
        SoundControl.instance.playSound("hurt");
        anim.SetTrigger("Hit");                                                     
        r2d.velocity = Vector2.up * 5f;                                             
        StartCoroutine(flash());                                                    
    }

    void ResetGame() 
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("restart scene"); 
    }

    public void death()
    {
        while (playerHealth > 0)
        {
            GM.instance.deleteHealth(playerHealth, transform.tag == "Player" ? 1 : 2);
            playerHealth -= 1;
        }
    }
    IEnumerator flash()
    {
        isInvincibility = true; 
        int num = 0;
        while (num < 5)             
        {
            yield return new WaitForSeconds(flashTime);     
            GetComponent<SpriteRenderer>().enabled = false;                     
            yield return new WaitForSeconds(flashTime);     
            GetComponent<SpriteRenderer>().enabled = true;          
            num++;
        }
        isInvincibility = false;    
    }

    public void setInvincible(bool isInvin)
    {
        isInvincibility = isInvin;
    }

}