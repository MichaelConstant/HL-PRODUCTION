using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KouKou : CharacterControl
{
    PlayerControl Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        isAlive = true;
        anim.SetBool("IsAlive",isAlive);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        CommonShoot();

        AttackVector = (Player.transform.position - transform.position).normalized;
        rb.velocity = AttackVector * movementSpeed_Final;

        if(rb.velocity.x > 0.01f)
        {
            sr.flipX = true;
            transform.GetChild(0).transform.localPosition = new Vector2(0.06f, 0.08f);
        }
        else
        {
            sr.flipX = false;
            transform.GetChild(0).transform.localPosition = new Vector2(-0.06f, 0.08f);
        }

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);

        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            isAlive = false;
            anim.SetBool("IsAlive", isAlive);
            anim.Play("Dead");
        }
        if (Info.normalizedTime > 1f && currentHP <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}
