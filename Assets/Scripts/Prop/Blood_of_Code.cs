using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood_of_Code : PropBase
{
    public float RangeDamageUpPercent;
    public float RangeAttackSpeedUpPercent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
        if (Player != null)
        {
            Player.rangeDamage_Ratio += RangeDamageUpPercent;
            Player.rangeDamage_Final = Player.rangeDamage_Basic * Player.rangeDamage_Ratio;
            Player.ShootInterval_Ratio += RangeAttackSpeedUpPercent;
            Player.ShootInterval_Final = Player.ShootInterval_Basic / Player.ShootInterval_Ratio;
            Destroy(gameObject);
        }
    }
}
