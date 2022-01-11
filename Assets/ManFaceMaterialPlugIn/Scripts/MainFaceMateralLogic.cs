using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFaceMateralLogic :EnemyBaseUnit 
{
    public enum BossState
    {
        Start,
        Idle,
        Jumping,
        Pour,
        Transforming,
        Shoot
    }

    GameObject m_player;
    Rigidbody2D m_rigidbody;
    Animator anim;

    public BossState m_bossState = BossState.Idle;
    public float m_moveSpeed;
    public GameObject bossUI;
    public int IdleToJumpRate;
    public int JumpToPourRate;
    public int JumpToIdleRate;
    public GameObject bigPhlegm;

    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_rigidbody = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Open Boss's HP UI
        bossUI.SetActive(true);
        //Wait for some time

    }

    public void SetBossState(BossState bossState)
    {
        m_bossState = bossState;
        if (m_bossState == BossState.Idle)
        {
            if (SetRate(IdleToJumpRate))
            {
                anim.Play("Jump");
                m_bossState = BossState.Jumping;
            }
        }
        if (m_bossState == BossState.Jumping)
        {
            if (SetRate(JumpToIdleRate))
            {
                anim.Play("Idle");
                m_bossState = BossState.Idle;
            }
            else if (SetRate(JumpToPourRate + JumpToIdleRate))
            {
                anim.Play("LookDownPour");
                m_bossState = BossState.Pour;
            }
        }
        if (m_bossState == BossState.Pour)
        {
            anim.Play("Jump");
            m_bossState = BossState.Jumping;
        }
    }
    //a Rate-Setting machine
    public bool SetRate(int coreNumber)
    {
        int i = Random.Range(1, 10);
        if (i < coreNumber)
        {
            return true;
        }
        return false;
    }
    public void JumpMovement()
    {
        if (m_bossState == BossState.Jumping)
        {
            if (m_rigidbody)
            {
                m_rigidbody.velocity = Vector2.zero;
                Vector2 enemyDirection = getDirection(m_player.transform);
                m_rigidbody.AddForce(enemyDirection * m_moveSpeed);
            }
        }
    }
    
   private void SetJumpState()
    {
        this.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        SetBossState(BossState.Jumping);
    }
    private void SetIdleState()
    {
        SetBossState(BossState.Idle);
    }
    private void SetPourState()
    {
        Instantiate(bigPhlegm, this.transform.position, Quaternion.identity);
        SetBossState(BossState.Pour);
    }
    public void TurnHead()
    {
        Vector2 RightUp = new Vector2(1, 1);
        Vector2 RightDown = new Vector2(1, -1);
        Vector3 tempVec = Vector3.Cross(RightDown, m_player.transform.position - transform.position);//Sin值
        float value = Vector3.Dot(RightDown, m_player.transform.position - this.transform.position);//Cos值
        anim.SetFloat("temp", tempVec.z);
        anim.SetFloat("value", value);
        ////主角在敌人右方
        //if (tempVec.z > 0 && value > 0)

        ////主角在敌人下方
        //if (tempVec.z < 0 && value > 0)

        ////主角在敌人上方
        //if (tempVec.z > 0 && value < 0)

        ////主角在敌人左方
        //if (tempVec.z < 0 && value < 0)
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{ 
    //        if (collision.gameObject.GetComponent<Bullet>() != null)
    //        {
    //            TakeDamage(bulletPower);
    //            if (currentHealth <= 0)
    //            {
    //                anim.SetBool("isDead", true);
    //                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //            }
    //        }
    //}
    //public void TakeDamage(float damage)
    //    {
    //        currentHealth -= damage;
    //        healthBar.SetHealth(currentHealth);
    //    }
    }
