using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Spawning,
    Attacking
}

public class SmallEnemyLogic : EnemyBaseUnit
{
    public Transform playerPos;
    Rigidbody2D m_rigidBody = null;

    EnemyState m_enemyState = EnemyState.Spawning;

    Vector2 m_attackDirection = Vector2.zero;

    public float m_movementSpeed ;
    Vector2 m_movementVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //敌人生成后自动进入攻击状态，攻击状态移动
        if (m_enemyState == EnemyState.Attacking)
        {
            m_movementVelocity = m_rigidBody.position + m_attackDirection.normalized * m_movementSpeed * Time.deltaTime;
            m_rigidBody.MovePosition(m_movementVelocity);
        }
    }

    //敌人攻击时朝主角走去
    public void SetEnemyState(EnemyState enemyState)
    {
        m_enemyState = enemyState;

        if(m_enemyState == EnemyState.Attacking)
        {
            m_attackDirection = getDirection(playerPos);
        }
    }
    //敌人碰到主角，主角受伤
    private void OnCollisionEnter2D(Collision2D collision)
    {

     //碰到主角，主角受伤
        if(collision.gameObject.tag == "Player")
        {

        }
        //碰到任何东西往反方向走
        m_attackDirection = getDirection(playerPos);
    }
}
