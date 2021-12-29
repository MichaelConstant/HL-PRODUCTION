using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (PlayerControl.MeleeEnergy < PlayerControl.MeleeEnergyMax)
            {
                PlayerControl.MeleeEnergy += 1;
            }
            if(PlayerControl.MeleeEnergy >= PlayerControl.MeleeEnergyMax && PlayerControl.MeleeLevel < PlayerControl.MeleeLevelMax)
            {
                PlayerControl.MeleeEnergy = PlayerControl.MeleeEnergy - PlayerControl.MeleeEnergyMax;
                PlayerControl.MeleeLevel += 1;
            }
            if (PlayerControl.MeleeEnergy >= PlayerControl.MeleeEnergyMax && PlayerControl.MeleeLevel >= PlayerControl.MeleeLevelMax)
            {
                PlayerControl.MeleeEnergy = PlayerControl.MeleeEnergyMax;
            }
        }
    }
}
