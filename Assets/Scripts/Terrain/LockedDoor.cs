using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    RoomController RoomController;
    PlayerControl Player;
    private void Start()
    {
        RoomController = GetComponentInParent<RoomController>();
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        Player = collision.gameObject.GetComponent<PlayerControl>();
        if (Player != null)
        {
            if(Player.KeyCounts>=1)
            {
                Player.KeyCounts--;
                Player.keyText.text = "Key: " + Player.KeyCounts;
                RoomController.OpenTheGay();
            }
        }
    }
}