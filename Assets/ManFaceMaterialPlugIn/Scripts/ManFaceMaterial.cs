using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManFaceMaterial : MainFaceMateralAnimation
{

    public GameObject bigPhlegm;

    public GameObject bossUI;

    private int idleTime = 0;
    private int jumpTime = 0;
    private int wholeJumpTime = 0;
    public int timeToJump;
    private int timeToIdle;
    public int idleMinTime;
    public int idleMaxTime;
    public int pourMinTime;
    public int pourMaxTime;
    private int timeToPour;

    public float bulletPower;

    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;

    private Animator anim;
    public Transform playerPos;
    public float waitTime;
    private float nextTime;
    private Rigidbody2D rb;
    public float MoveSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        bossUI.SetActive(true);
        if (Time.time > nextTime)
        {
            nextTime = Time.time + waitTime;
            anim.SetTrigger("jumpTrigger");
        }
        timeToIdle = Random.Range(idleMinTime, idleMaxTime);
        timeToPour = Random.Range(pourMinTime, pourMaxTime);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        nextTime = waitTime;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
    }
    void Update()
    {

    }
    public void Jump()
    {
        Debug.Log($"idleTime:{idleTime},jumpTime:{ jumpTime},wholeJumpTime:  {wholeJumpTime}");
        idleTime = 0;
        Vector2 enemyDirection = getDirection(playerPos);
        rb.AddForce(enemyDirection * MoveSpeed);
    }
    public void TurnHead()
    {
        Vector2 RightDown = new Vector2(1, -1);
        Vector3 tempVec = Vector3.Cross(RightDown, playerPos.position - transform.position);//Sinֵ
        float value = Vector3.Dot(RightDown, playerPos.position - this.transform.position);//Cosֵ
        anim.SetFloat("temp", tempVec.z);
        anim.SetFloat("value", value);
    }
    public void StopJump()
    {
        this.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;

        jumpTime++;
        wholeJumpTime++;
        if (jumpTime > timeToIdle)
        {
            jumpTime = 0;
            anim.SetTrigger("idleTrigger");
        }
        else if (wholeJumpTime > timeToPour)
        {
            anim.SetTrigger("pourTrigger");
            jumpTime = 0;
            wholeJumpTime = 0;
        }

    }
    public void CountIdleTime()
    {
        idleTime++;
        if (idleTime > timeToJump)
        {
            anim.SetTrigger("jumpTrigger");
        }
    }

    public void LookDownPour()
    {
        Instantiate(bigPhlegm, this.transform.position, Quaternion.identity);
        timeToIdle = Random.Range(idleMinTime, idleMaxTime);
        timeToPour = Random.Range(pourMinTime, pourMaxTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            TakeDamage(bulletPower);
            if (currentHealth <=0)
            {
                anim.SetBool("isDead",true);
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}