using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 3.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;


    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset / enemyPatroltime, 0);
    }
    void MoveEnemy(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {

        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move Enemy
            MoveEnemy();
        }
        else{
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }
  }
}