using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : CharacterControl
{
    public int EnemyHP = 10;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BOSS HP: " + EnemyHP);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canShoot)
        {
            canShoot = false;
            Shoot();
        }
    }
}
