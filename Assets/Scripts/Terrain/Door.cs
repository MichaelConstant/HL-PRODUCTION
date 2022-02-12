using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    RoomGenerator room_Generator;
    LevelManager level_Manager;
    RoomController room_controller;

    public Sprite defaultDoorUp;
    public Sprite defaultDoorDown;
    public Sprite defaultDoorLeft;
    public Sprite defaultDoorRight;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SpriteRenderer sr;

    public enum DoorDirection { up, down, left, right };
    public DoorDirection doorDirection;

    public enum RoomTypeAside { StartRoom, ProgramRoom, ArtRoom, DesignRoom, BossRoom, RandEncoRoom };
    //0,1,2,3,4,5,*6*
    public RoomTypeAside roomTypeAside;

    private void Awake()
    {
        room_Generator = GetComponentInParent<RoomGenerator>();
        level_Manager = GetComponentInParent<LevelManager>();
        room_controller = GetComponentInParent<RoomController>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        switch (doorDirection)
        {
            case DoorDirection.up:
                sr.sprite = defaultDoorUp;
                break;
            case DoorDirection.down:
                sr.sprite = defaultDoorDown;
                break;
            case DoorDirection.left:
                sr.sprite = defaultDoorLeft;
                break;
            case DoorDirection.right:
                sr.sprite = defaultDoorRight;
                break;
        }
    }
    public void ShutTheGay()
    {
        if (room_controller.roomType != RoomController.RoomType.StartRoom)
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
        }
        else
        {
            switch (doorDirection)
            {
                case DoorDirection.up:
                    anim.Play("Green_Idle_Up");
                    break;
                case DoorDirection.down:
                    anim.Play("Green_Idle_Down");
                    break;
                case DoorDirection.left:
                    anim.Play("Green_Idle_Left");
                    break;
                case DoorDirection.right:
                    anim.Play("Green_Idle_Right");
                    break;
            }
        }
    }
    public void TurnOnTheGay()
    {

    }
}