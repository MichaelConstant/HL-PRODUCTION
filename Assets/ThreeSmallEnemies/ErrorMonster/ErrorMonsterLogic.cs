using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ErrorMonsterLogic : CharacterControl
{
    PlayerControl Player;
    public enum EnemyState
    {
        Spawning,
        Static,
        Hiding,
        Transforming,
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);
        if (Info.normalizedTime > 1f)
        {
            if (currentHP <= 0)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);
            }

        }
        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            anim.Play("Die");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            anim.Play("GetHurt");
        }
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            anim.Play("Static_1");
            ErrorShoot();
        }
    }
    void ErrorShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            for (int i = 0; i < 8; i++)
            {
                Instantiate(bullet_0, bulletSpawn.transform.position, transform.rotation);
                transform.GetChild(0).transform.Rotate(0, 0, 45);
            }
        }
    }
}