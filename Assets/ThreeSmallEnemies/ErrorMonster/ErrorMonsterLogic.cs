using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ErrorMonsterLogic : CharacterControl
{
    PlayerControl Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canShoot = false;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        AttackVector = (Player.transform.position - transform.position).normalized;
        rb.velocity = AttackVector * movementSpeed_Final;

        ErrorShoot();

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);
        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            anim.Play("Die");
        }
        if (Info.normalizedTime > 1f && currentHP <= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
    void ErrorShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            for (int i = 0; i < 8; i++)
            {
                Instantiate(bullet_0, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                transform.GetChild(0).transform.Rotate(0, 0, 45);
            }
        }
    }
}