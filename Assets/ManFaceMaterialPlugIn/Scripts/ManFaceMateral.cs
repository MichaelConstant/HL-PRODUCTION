using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManFaceMateral : CharacterControl
{
    int state;
    int lastState = 0;
    int jumpCount = 0;
    //0:Idle, 1:Jump, 2:Pour
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AttackVector = GameObject.FindWithTag("Player").transform.position - transform.position;
        Idle();
    }
    private void Update()
    {
        AttackVector = GameObject.FindWithTag("Player").transform.position - transform.position;
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            int rand = Random.Range(0, 100);

            if (rand >= 0 && rand < 80)
            {
                state = 1;
            }
            else if (rand >= 80 && rand < 90)
            {
                if (lastState == 0)
                {
                    state = 1;
                }
                else
                {
                    state = 0;
                }
            }
            else if (rand >= 90 && rand < 100)
            {
                if(lastState == 2)
                {
                    state = 1;
                }
                else
                {
                    state = 2;
                }
            }

            switch (state)
            {
                case 0:
                    Idle();
                    lastState = 0;
                    break;
                case 1:
                    StartCoroutine(Jump());
                    lastState = 1;
                    break;
                case 2:
                    LookDownPour();
                    lastState = 2;
                    break;
            }
        }
    }

    private IEnumerator Jump()
    {
        rb.AddForce(transform.up * 98000000 + new Vector3(AttackVector.x, AttackVector.y, 0) * movementSpeed);
        rb.gravityScale = 0.4f;
        anim.Play("Jump");
        jumpCount++;
        yield return new WaitForSeconds(0.98f);
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void Idle()
    {
        anim.Play("Idle");
    }

    private void LookDownPour()
    {
        anim.Play("LookDownPour");
        CommonShoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collisionRB = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.GetComponent<PlayerControl>()!=null)
        {
            Debug.Log("111");
            collisionRB.AddForce((collision.gameObject.transform.position- gameObject.transform.position).normalized * 900f);
        }
    }
}