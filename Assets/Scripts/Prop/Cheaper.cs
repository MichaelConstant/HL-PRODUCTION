using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheaper : PropBase
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
        if (Player != null)
        {
            Player.gameObject.AddComponent<Cheaper_Buff>();
            Destroy(gameObject);
        }
    }
}