using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Lag_Shooter : Shooter_Base
{
    public override void Fire()
    {
        if (Player.canShoot)
        {
            Player.canShoot = false;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            transform.Rotate(0, 0, Random.Range(-180,180));

            switch (Player.MeleeLevel)
            {
                case 0:
                    GameObject bullet = Instantiate(Player.bullet_0, Player.bulletSpawn.transform.position, Player.bulletSpawn.transform.rotation);
                    bullet.transform.parent = Player.transform;
                    break;
                case 1:
                    bullet = Instantiate(Player.bullet_1, Player.bulletSpawn.transform.position, Player.bulletSpawn.transform.rotation);
                    bullet.transform.parent = Player.transform;
                    break;
                case 2:
                    bullet = Instantiate(Player.bullet_2, Player.bulletSpawn.transform.position, Player.bulletSpawn.transform.rotation);
                    bullet.transform.parent = Player.transform;
                    break;
                case 3:
                    bullet = Instantiate(Player.bullet_3, Player.bulletSpawn.transform.position, Player.bulletSpawn.transform.rotation);
                    bullet.transform.parent = Player.transform;
                    break;
            }
        }
    }
}