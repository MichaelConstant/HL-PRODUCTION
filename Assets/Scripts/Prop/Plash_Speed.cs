using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plash_Speed : PropBase
{
    public float SpeedUpPercent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
        if (Player != null)
        {
            Player.movementSpeed_Ratio += SpeedUpPercent;
            Player.movementSpeed_Final = Player.movementSpeed_Basic * Player.movementSpeed_Ratio;
            Destroy(gameObject);
        }
    }
}