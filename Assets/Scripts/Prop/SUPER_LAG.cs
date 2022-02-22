using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Super_Lag : PropBase
{
    public float RangeAttackSpeedUpPercent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            GameObject Player = collision.gameObject;
            GameObject PlayerShooter = collision.transform.GetChild(1).gameObject;

            Player.GetComponent<PlayerControl>().ShootInterval_Ratio += RangeAttackSpeedUpPercent;
            Player.GetComponent<PlayerControl>().ShootInterval_Final = Player.GetComponent<PlayerControl>().ShootInterval_Basic / Player.GetComponent<PlayerControl>().ShootInterval_Ratio;

            Destroy(PlayerShooter.GetComponent<RageShooter_Base>());

            PlayerShooter.AddComponent<Super_Lag_RageShooter>();

            Player.GetComponent<PlayerControl>().RageShooter = Player.GetComponentInChildren<Super_Lag_RageShooter>();

            Destroy(gameObject);
        }
    }
}