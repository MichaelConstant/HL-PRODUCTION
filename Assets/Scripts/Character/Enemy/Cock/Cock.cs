using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cock : CharacterControl
{
    PlayerControl Player;
    GameObject BossHP;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        BossHP = GameObject.FindWithTag("GameUI").transform.GetChild(0).gameObject;
        BossHP.SetActive(true);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        BossHP.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)currentHP / maxHP;

        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        AttackVector = (Player.transform.position - transform.position).normalized;

        if (rb.velocity.x > 0.01f)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x < -0.01f)
        {
            sr.flipX = false;
        }

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);

        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            isAlive = false;
            anim.Play("Dead");
        }
        if (Info.normalizedTime > 1f && currentHP <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject);
            BossHP.SetActive(true);
        }

        if (Info.normalizedTime > 1f && canShoot)
        {
            rb.velocity = new Vector3(0, 0, 0);
            anim.Play("Attack");
            canShoot = false;
        }

        if (Info.normalizedTime > 1f && !canShoot)
        {
            rb.velocity = AttackVector * movementSpeed_Final;
            anim.Play("Walk");
        }
    }
}