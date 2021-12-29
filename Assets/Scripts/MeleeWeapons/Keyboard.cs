using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
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
            Destroy(collision.gameObject);
            if (PlayerControl.MeleeEnergyStatic < PlayerControl.MeleeEnergyMaxStatic)
            {
                PlayerControl.MeleeEnergyStatic += PlayerControl.MeleeEnergyPerHitStatic;
            }
            if(PlayerControl.MeleeEnergyStatic >= PlayerControl.MeleeEnergyMaxStatic && PlayerControl.MeleeLevelStatic < PlayerControl.MeleeLevelMaxStatic)
            {
                PlayerControl.MeleeLevelStatic += 1;
                if (PlayerControl.MeleeLevelStatic < PlayerControl.MeleeLevelMaxStatic)
                {
                    PlayerControl.MeleeEnergyStatic = PlayerControl.MeleeEnergyStatic - PlayerControl.MeleeEnergyMaxStatic + PlayerControl.MeleeEnergyMaxStatic * 0.05f;
                }
            }
            if (PlayerControl.MeleeEnergyStatic >= PlayerControl.MeleeEnergyMaxStatic && PlayerControl.MeleeLevelStatic >= PlayerControl.MeleeLevelMaxStatic)
            {
                PlayerControl.MeleeEnergyStatic = PlayerControl.MeleeEnergyMaxStatic;
            }
        }
    }
}
