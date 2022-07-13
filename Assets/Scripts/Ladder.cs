using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Collider2D collider;
    public float upSpeed = 5f;
    private string enterTag;
    public float min;
    public float max;

    public bool isDown;
    public bool isUp;
    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Update() {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Player" && Input.GetKey(KeyCode.W)) || (collision.tag == "Player2" && Input.GetKey(KeyCode.UpArrow))) //dect collision with player
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            enterTag = collision.tag;
            // collision.GetComponent<Rigidbody2D>().velocity = Vector2.up * upSpeed * Time.deltaTime;
            collision.transform.position += Vector3.up * upSpeed * Time.deltaTime;
            collider.enabled = false;                   //disable the collider of the top of the ladder
        }
        if ((collision.tag == "Player" && Input.GetKey(KeyCode.S)) || (collision.tag == "Player2" && Input.GetKey(KeyCode.DownArrow)))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            enterTag = collision.tag;
            collision.transform.position += -Vector3.up * upSpeed * Time.deltaTime;
            //collision.GetComponent<Rigidbody2D>().velocity = -Vector2.up * upSpeed * Time.deltaTime;
            collider.enabled = false;                   //disable the collider of the top of the ladder
        }
        if (collision.tag == "Enemy" && isDown == false)
        {
            up(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == enterTag)
        {
            collider.enabled = true;        //enable the collider of the top of the ladder
            collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    public void up(Collider2D collision)
    {
        if (isUp == true || isDown == true)
            return;

        int rand = (int)Random.Range(min, max);
        print(rand);
        if (rand > 5)
        {
            StartCoroutine(EnemyUp(collision));
        }
    }
    public void down(Collider2D collision)
    {
        if (isUp == true || isDown == true)
            return;
        if (collision.tag == "Player" || collision.tag == "Player2")
            collider.enabled = false;
        if (collision.tag == "Enemy")
        {
            int rand = (int)Random.Range(min, max);
            print(rand);
            if (rand > 5)
            {
                StartCoroutine(EnemyDown(collision));
            }
        }
    }

    IEnumerator EnemyDown(Collider2D collision)
    {
        float time = 0;
        isDown = true;
        while (time < 5)
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            yield return new WaitForSeconds(0.7f);
            collision.GetComponent<Ai>().currentState = Ai.State.IDEL;
            collision.GetComponent<Rigidbody2D>().velocity = -Vector2.up * upSpeed * 0.2f;
            collider.enabled = false;
            time += 0.7f;
            print(time);
        }
        collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        collision.GetComponent<Ai>().currentState = Ai.State.WALK;
        yield return new WaitForSeconds(1.5f);
        isDown = false;
        collider.enabled = true;
    }

    IEnumerator EnemyUp(Collider2D collision)
    {
        float time = 0;
        isUp = true;
        while (time < 4)
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            yield return new WaitForSeconds(1f);
            collision.GetComponent<Ai>().currentState = Ai.State.IDEL;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.up * upSpeed * 0.2f;
            collider.enabled = false;
            time += 0.7f;
            print(time);
        }
        collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        collision.GetComponent<Ai>().currentState = Ai.State.WALK;
        yield return new WaitForSeconds(1.5f);
        isUp = false;
        collider.enabled = true;
    }
}
