using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Sprite defaultDoorUp;
    public Sprite defaultDoorDown;
    public Sprite defaultDoorLeft;
    public Sprite defaultDoorRight;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public RoomController room_Controller;

    public enum DoorDirection { up, down, left, right };
    public DoorDirection doorDirection;

    public enum RoomTypeAside { StartRoom, ProgramRoom, ArtRoom, DesignRoom, BossRoom, RandEncoRoom, None };
    //0,1,2,3,4,5,*6*
    public RoomTypeAside roomTypeAside;

    bool roomAsideEntered;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (roomTypeAside == RoomTypeAside.None)
        {
            Destroy(GetComponent<SpriteRenderer>());
        }
        else
        {
            if (roomTypeAside == (Door.RoomTypeAside) RoomController.RoomType.BossRoom)
            {
                switch (doorDirection)
                {
                    case DoorDirection.up:
                        anim.Play("Boss_Idle_Up");
                        break;
                    case DoorDirection.down:
                        anim.Play("Boss_Idle_Down");
                        break;
                    case DoorDirection.left:
                        anim.Play("Boss_Idle_Left");
                        break;
                    case DoorDirection.right:
                        anim.Play("Boss_Idle_Right");
                        break;
                }
            }
            else
            {
                switch (doorDirection)
                {
                    case DoorDirection.up:
                        anim.Play("Opened_Idle_Up");
                        break;
                    case DoorDirection.down:
                        anim.Play("Opened_Idle_Down");
                        break;
                    case DoorDirection.left:
                        anim.Play("Opened_Idle_Left");
                        break;
                    case DoorDirection.right:
                        anim.Play("Opened_Idle_Right");
                        break;
                }
            }
        }

        switch (doorDirection)
        {
            case DoorDirection.up:
                Destroy(gameObject.transform.GetChild(1).gameObject);
                Destroy(gameObject.transform.GetChild(2).gameObject);
                Destroy(gameObject.transform.GetChild(3).gameObject);
                break;
            case DoorDirection.down:
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Destroy(gameObject.transform.GetChild(2).gameObject);
                Destroy(gameObject.transform.GetChild(3).gameObject);
                break;
            case DoorDirection.left:
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Destroy(gameObject.transform.GetChild(1).gameObject);
                Destroy(gameObject.transform.GetChild(3).gameObject);
                break;
            case DoorDirection.right:
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Destroy(gameObject.transform.GetChild(1).gameObject);
                Destroy(gameObject.transform.GetChild(2).gameObject);
                break;
        }
    }
    private void FixedUpdate()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null)
        {
            roomAsideEntered = RoomGenerator.RoomList.Find(Room => Room.RoomLocation == GetComponentInParent<RoomController>().ChangePos((int)doorDirection, transform.position)).RoomEntered;
            anim.SetBool("RoomAsideEntered", roomAsideEntered);
        }
    }
    public void ShutTheGay()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            switch (doorDirection)
            {
                case DoorDirection.up:
                    anim.Play("Disappear_Up");
                    break;
                case DoorDirection.down:
                    anim.Play("Disappear_Down");
                    break;
                case DoorDirection.left:
                    anim.Play("Disappear_Left");
                    break;
                case DoorDirection.right:
                    anim.Play("Disappear_Right");
                    break;
            }
            gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }
    public void TurnOnTheGay()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            if (roomTypeAside == (Door.RoomTypeAside)RoomController.RoomType.BossRoom)
            {
                switch (doorDirection)
                {
                    case DoorDirection.up:
                        anim.Play("Boss_Appear_Up");
                        break;
                    case DoorDirection.down:
                        anim.Play("Boss_Appear_Down");
                        break;
                    case DoorDirection.left:
                        anim.Play("Boss_Appear_Left");
                        break;
                    case DoorDirection.right:
                        anim.Play("Boss_Appear_Right");
                        break;
                }
            }
            else
            {
                switch (doorDirection)
                {
                    case DoorDirection.up:
                        anim.Play("Opened_Appear_Up");
                        break;
                    case DoorDirection.down:
                        anim.Play("Opened_Appear_Down");
                        break;
                    case DoorDirection.left:
                        anim.Play("Opened_Appear_Left");
                        break;
                    case DoorDirection.right:
                        anim.Play("Opened_Appear_Right");
                        break;
                }
            }
            gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}