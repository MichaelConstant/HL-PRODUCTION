using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maHuaBullet : EnemyBullet
{

    private Rigidbody2D rb;
    private Animator anim;
    public override void Start()
    {
        base.Start();
        canBeObstacled = false;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        BulletVector = (Player.transform.position - gameObject.transform.position);
        StartCoroutine(Fire());
    }
    private IEnumerator Fire()
    {
        rb.AddForce((Vector2)transform.up * 100f + BulletVector.normalized * BulletSpeed);
        rb.gravityScale = 0.4f;

        yield return new WaitForSeconds(0.98f);
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);
        anim.Play("MaHuaBullet");
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Destroy(this.gameObject);
        }
    }
}
