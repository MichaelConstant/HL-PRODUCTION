using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void OnEnable()
    {
        if (Player.GetComponent<PlayerControl>().canShoot)
        {
            Player.GetComponent<PlayerControl>().MeleeEnergyDecreaseOfShooting();
            Player.GetComponent<PlayerControl>().canShoot = false;
            Player.GetComponent<PlayerControl>().AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
            if (Player.GetComponent<PlayerControl>().onRage)
            {
                switch (PlayerControl.MeleeLevel_Static)
                {
                    case 0:
                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) - 5);
                        GameObject bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;

                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) + 5);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                    case 1:
                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) - 15);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;

                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector));
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;

                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) + 15);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                    case 2:
                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) - 5);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;

                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) + 5);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;

                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) - 15);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;

                        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector) + 15);
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                }
            }
            else
            {
                GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector));

                switch (PlayerControl.MeleeLevel_Static)
                {
                    case 0:
                        GameObject bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_0, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                    case 1:
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_1, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                    case 2:
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_2, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                    case 3:
                        bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_3, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
                        bullet.transform.parent = Player.transform;
                        break;
                }
            }
            StartCoroutine(Player.GetComponent<PlayerControl>().ShootInterval());
        }
        GetComponent<Shooter>().enabled = false;
    }
}