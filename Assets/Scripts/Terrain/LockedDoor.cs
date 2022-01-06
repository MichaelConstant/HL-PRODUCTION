using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            if(collision.gameObject.GetComponent<PlayerControl>().KeyCounts>=1)
            {
                collision.gameObject.GetComponent<PlayerControl>().KeyCounts--;
                RoomGenerator.OpenTheGay();
            }
        }
    }
}
