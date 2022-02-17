using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBullet : Bullet
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed);
    }
}
