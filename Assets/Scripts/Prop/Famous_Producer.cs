using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Famous_Producer : PropBase
{
    public float MeleeEnergyPerHitIncreased;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
        if (Player != null)
        {
            Player.MeleeEnergyPerHit += MeleeEnergyPerHitIncreased;
            Destroy(gameObject);
        }
    }
}