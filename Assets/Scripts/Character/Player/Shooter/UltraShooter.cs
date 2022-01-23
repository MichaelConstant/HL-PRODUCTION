using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraShooter : MonoBehaviour
{
    PlayerControl Player;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }
    private void OnEnable()
    {
        Player.AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
        GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Player.GetComponent<PlayerControl>().Angle_360(Player.GetComponent<PlayerControl>().AttackVector));
        GameObject bullet = Instantiate(Player.GetComponent<PlayerControl>().bullet_Ultra, Player.GetComponent<PlayerControl>().bulletSpawn.transform.position, Player.GetComponent<PlayerControl>().bulletSpawn.transform.rotation);
        bullet.transform.parent = Player.transform;
        GetComponent<UltraShooter>().enabled = false;
    }
}
