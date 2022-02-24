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
        rb.velocity = new Vector2(0,0);
        StartCoroutine(StartRolling());
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        rb.velocity = rb.velocity.normalized * movementSpeed_Final;

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);

        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.Play("Die");
        }
        if (Info.normalizedTime > 1f && currentHP <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        AttackVector = (Player.transform.position - transform.position).normalized;

        if (collision.gameObject.tag == "Terrain"|| collision.gameObject.layer == 8)
        {
            rb.velocity = AttackVector * movementSpeed_Final;
        }
        else if (collision.gameObject.GetComponent<CharacterControl>() != null)
        {
            rb.velocity = (new Vector2(-rb.velocity.x, -rb.velocity.y)).normalized * movementSpeed_Final;
        }
    }

    IEnumerator StartRolling()
    {
        yield return new WaitForSeconds(1f);
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        AttackVector = (Player.transform.position - transform.position).normalized;
        rb.velocity = AttackVector * movementSpeed_Final;
    }
}