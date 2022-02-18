using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    Door door;
    RoomController room;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        door = GetComponentInParent<Door>();
        room = GetComponentInParent<RoomController>();
        if (collision.gameObject.GetComponent<PlayerControl>() != null && room.rewarded && door.roomAsideLocked)
        {
            PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
            if (Player.KeyCounts > 0)
            {
                Player.KeyCounts--;
                Player.keyText.text = "Key: " + Player.KeyCounts;
                RoomGenerator.RoomList.Find(Room => Room.RoomLocation == GetComponentInParent<RoomController>().ChangePos((int)door.doorDirection, transform.position)).RoomLocked = false;
                door.roomAsideLocked = true;
                GetComponent<BoxCollider2D>().enabled = false;
                switch (door.doorDirection)
                {
                    case Door.DoorDirection.up:
                        door.anim.Play("Unlock_Up");
                        break;
                    case Door.DoorDirection.down:
                        door.anim.Play("Unlock_Down");
                        break;
                    case Door.DoorDirection.left:
                        door.anim.Play("Unlock_Left");
                        break;
                    case Door.DoorDirection.right:
                        door.anim.Play("Unlock_Right");
                        break;
                }
            }
        }
    }
}
