using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : CharacterControl
{
    PlayerControl Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        AttackVector = (Player.transform.position - transform.position).normalized;
        rb.velocity =AttackVector * movementSpeed_Final;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);

        if (Info.normalizedTime > 1f && currentHP <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.Play("Die");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        AttackVector = (Player.transform.position - transform.position).normalized;

        if (collision.gameObject.tag == "Terrain")
        {
            rb.velocity = AttackVector * movementSpeed_Final;
        }
        else if (collision.gameObject.GetComponent<CharacterControl>() != null)
        {
            rb.velocity = (new Vector2(-rb.velocity.x, -rb.velocity.y)).normalized * movementSpeed_Final;
        }
    }
}