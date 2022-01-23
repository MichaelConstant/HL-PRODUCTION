using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    RoomController RoomController;
    private void Start()
    {
        RoomController = GetComponentInParent<RoomController>();
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            if(collision.gameObject.GetComponent<PlayerControl>().KeyCounts>=1)
            {
                collision.gameObject.GetComponent<PlayerControl>().KeyCounts--;
                collision.gameObject.GetComponent<PlayerControl>().keyText.text = "Key: " + collision.gameObject.GetComponent<PlayerControl>().KeyCounts;
                RoomController.OpenTheGay();
            }
        }
    }
}
