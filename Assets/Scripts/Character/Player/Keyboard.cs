using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    //public GameObject ParriedBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyBullet>()==true)
        {
            //GameObject tempBullet = Instantiate(ParriedBullet,transform.position,Quaternion.identity);
            //tempBullet.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<SpriteRenderer>().sprite;
            //tempBullet.GetComponent<ParriedBullet>().BulletDamage = collision.GetComponent<EnemyBullet>().BulletDamage;
            //tempBullet.GetComponent<ParriedBullet>().BulletSpeed = collision.GetComponent<EnemyBullet>().BulletSpeed;
            Destroy(collision.gameObject);
            if (!PlayerControl.onRage)
            {
                if (PlayerControl.MeleeEnergyStatic < PlayerControl.MeleeEnergyMaxStatic)
                {
                    PlayerControl.MeleeEnergyStatic += PlayerControl.MeleeEnergyPerHitStatic;
                }
                if (PlayerControl.MeleeEnergyStatic >= PlayerControl.MeleeEnergyMaxStatic && PlayerControl.MeleeLevelStatic < PlayerControl.MeleeLevelMaxStatic)
                {
                    PlayerControl.MeleeLevelStatic += 1;
                    PlayerControl.MeleeEnergyStatic = PlayerControl.MeleeEnergyStatic - PlayerControl.MeleeEnergyMaxStatic + PlayerControl.MeleeEnergyMaxStatic * PlayerControl.MeleeEnergyProtectPercentStatic;
                }
                if (PlayerControl.MeleeEnergyStatic >= PlayerControl.MeleeEnergyMaxStatic && PlayerControl.MeleeLevelStatic >= PlayerControl.MeleeLevelMaxStatic)
                {
                    PlayerControl.MeleeEnergyStatic = PlayerControl.MeleeEnergyMaxStatic;
                }
            }
        }
    }
}