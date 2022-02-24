
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : CharacterControl
{
    PlayerControl Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        isAlive = true;
        anim.SetBool("IsAlive", isAlive);
        canShoot = false;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AttackVector = (Player.transform.position - transform.position).normalized;
        if (isAlive)
        {
            CommonShoot();
        }
        rb.velocity = AttackVector * movementSpeed_Final;

        if (rb.velocity.x > 0.01f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);
        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isAlive = false;
            anim.Play("Dead");
        }

        if (Info.normalizedTime > 1f && currentHP <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}