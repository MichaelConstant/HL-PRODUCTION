using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscLogic : EnemyBaseUnit
{
    GameObject m_player;
    Rigidbody2D rb;
    public float m_rayLength;
    Vector2 enemyDirection;
    public float m_moveSpeed;
    void Start() 
    {
        rb = this.GetComponent<Rigidbody2D>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        enemyDirection = getDirection(m_player.transform);
        rb.velocity = (enemyDirection * m_moveSpeed);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            rb.velocity*= (-1);
        }
    }
}
