using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlashSpeed : PropBase
{
    public float SpeedUpPercent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerControl>()!=null)
        {
            collision.GetComponent<PlayerControl>().movementSpeed_Ratio += SpeedUpPercent;
            collision.GetComponent<PlayerControl>().movementSpeed_Final = collision.GetComponent<PlayerControl>().movementSpeed_Basic * collision.GetComponent<PlayerControl>().movementSpeed_Ratio;
            Destroy(gameObject);
        }
    }
}