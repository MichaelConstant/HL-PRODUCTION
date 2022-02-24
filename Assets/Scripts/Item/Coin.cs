using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PropBase
{
    public int CoinCount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerControl>()!=null)
        {
            collision.gameObject.GetComponent<PlayerControl>().CoinCounts++;
            collision.gameObject.GetComponent<PlayerControl>().coinText.text= "Coin: " + collision.gameObject.GetComponent<PlayerControl>().CoinCounts;
            Destroy(gameObject);
        }
    }
}