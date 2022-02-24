using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PropBase
{
    public GameObject minimapHeart;
    public int HealAmount;
    // Start is called before the first frame update
    void Start()
    {
        minimapHeart.transform.parent = null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null && collision.gameObject.GetComponent<PlayerControl>().currentHP < collision.gameObject.GetComponent<PlayerControl>().maxHP)
        {
            collision.gameObject.GetComponent<PlayerControl>().currentHP += HealAmount;
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Destroy(minimapHeart);
    }
}
