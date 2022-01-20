using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    GameObject Player;
    private void Awake()
    {
        Player = gameObject.GetComponentInParent<PlayerControl>().gameObject;
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
                    if (PlayerControl.MeleeEnergy_Static < PlayerControl.MeleeEnergyMax_Static)
                    {
                        PlayerControl.MeleeEnergy_Static += PlayerControl.MeleeEnergyPerHitStatic;
                    }
                    if (PlayerControl.MeleeEnergy_Static >= PlayerControl.MeleeEnergyMax_Static && PlayerControl.MeleeLevel_Static < PlayerControl.MeleeLevelMax_Static)
                    {
                        PlayerControl.MeleeLevel_Static += 1;
                        PlayerControl.MeleeEnergy_Static = PlayerControl.MeleeEnergy_Static - PlayerControl.MeleeEnergyMax_Static + PlayerControl.MeleeEnergyMax_Static * PlayerControl.MeleeEnergyProtectPercent_Static;
                    }
                    if (PlayerControl.MeleeEnergy_Static >= PlayerControl.MeleeEnergyMax_Static && PlayerControl.MeleeLevel_Static >= PlayerControl.MeleeLevelMax_Static)
                    {
                        PlayerControl.MeleeEnergy_Static = PlayerControl.MeleeEnergyMax_Static;
                    }
                }
            }
        }
    }
}