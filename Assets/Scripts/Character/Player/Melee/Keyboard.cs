using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    PlayerControl Player;
    private void Awake()
    {
        Player = gameObject.GetComponentInParent<PlayerControl>();
    }
    //public GameObject ParriedBullet;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyBullet>() != null)
        {
            if (collision.gameObject.GetComponent<EnemyBullet>().bulletType == 0)
            {
                //GameObject tempBullet = Instantiate(ParriedBullet,transform.position,Quaternion.identity);
                //tempBullet.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<SpriteRenderer>().sprite;
                //tempBullet.GetComponent<ParriedBullet>().BulletDamage = collision.GetComponent<EnemyBullet>().BulletDamage;
                //tempBullet.GetComponent<ParriedBullet>().BulletSpeed = collision.GetComponent<EnemyBullet>().BulletSpeed;
                Destroy(collision.gameObject);
                if (!Player.GetComponent<PlayerControl>().onRage)
                {
                    if (Player.MeleeEnergy < Player.MeleeEnergyMax)
                    {
                        Player.MeleeEnergy += Player.MeleeEnergyPerHit;
                    }
                    if (Player.MeleeEnergy >= Player.MeleeEnergyMax && Player.MeleeLevel < Player.MeleeLevelMax)
                    {
                        Player.MeleeLevel += 1;
                        Player.MeleeEnergy = Player.MeleeEnergy - Player.MeleeEnergyMax + Player.MeleeEnergyMax * Player.MeleeEnergyProtectPercent;
                    }
                    if (Player.MeleeEnergy >= Player.MeleeEnergyMax && Player.MeleeLevel >= Player.MeleeLevelMax)
                    {
                        Player.MeleeEnergy = Player.MeleeEnergyMax;
                    }
                }
            }
        }
    }
}