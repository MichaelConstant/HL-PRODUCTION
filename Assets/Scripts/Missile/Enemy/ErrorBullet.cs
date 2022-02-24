using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBullet : EnemyBullet
{
    public override void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GetComponent<Rigidbody2D>().AddForce(transform.up.normalized * BulletSpeed);
    }
}