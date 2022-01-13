using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManFaceMateral : CharacterControl
{
    int state;
    int lastState;
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
                state = 0;
            }
            else if (rand >= 90 && rand < 100)
            {
                state = 2;
            }
            switch (state)
            {
                case 0:
                    Idle();
                    break;
                case 1:
                    StartCoroutine(Jump());
                    break;
                case 2:
                    LookDownPour();
                    break;
            }
        }
    }

    private IEnumerator Jump()
    {
        rb.AddForce(transform.up * 10 + new Vector3(AttackVector.x, AttackVector.y, 0) * movementSpeed);
        rb.gravityScale = 0.4f;
        anim.Play("Jump");
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
}