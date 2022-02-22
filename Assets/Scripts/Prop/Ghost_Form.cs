using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Form : PropBase
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
        if (Player != null)
        {
            Player.gameObject.layer = 9;
            Destroy(gameObject);
        }
    }
}