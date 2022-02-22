using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PropBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            collision.gameObject.GetComponent<PlayerControl>().KeyCounts++;
            collision.gameObject.GetComponent<PlayerControl>().keyText.text = "Key: " + collision.gameObject.GetComponent<PlayerControl>().KeyCounts;
            Destroy(gameObject);
        }
    }
}
