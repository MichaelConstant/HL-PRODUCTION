using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float BulletDamage;
    public float BulletSpeed;
    public float BulletLifeSpan;
    public PlayerControl Player;
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        BulletDamage = Player.rangeDamage_Final;
    }
}