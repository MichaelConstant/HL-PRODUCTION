using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    float xOffset;
    float yOffset;

    LevelManager level_Manager;
    RoomGenerator room_Generator;
    SpriteRenderer sr;

    public enum RoomType { StartRoom, ProgramRoom, ArtRoom, DesignRoom, BossRoom, AudioRoom };
    //0,1,2,3,4,5
    public RoomType roomType;

    public Sprite Art;
    public Sprite Program;
    public Sprite Design;

    enum Direction { up, down, left, right };
    public GameObject DoorObject;

    Vector3 CamPos;
    Vector3 RoomPos;
    Vector3 cameraVector3 = Vector3.zero;

    bool spawned;
    public int spawnNum;
    
    bool rewarded;
    public int rewardNum;

    void Start()
    {
        level_Manager = GetComponentInParent<LevelManager>();
        room_Generator = GetComponentInParent<RoomGenerator>();
        sr = GetComponent<SpriteRenderer>();

        xOffset = room_Generator.xOffset;
        yOffset = room_Generator.yOffset;

        if (roomType == RoomType.StartRoom)
        {
            spawned = true;
            rewarded = true;
        }
        else
        {
            spawned = false;
            rewarded = false;
        }

        for (int i = 0; i < 4; i++)
        {
            if (RoomGenerator.RoomList.Exists(Room => Room.RoomLocation == ChangePos(i, transform.position)))
            {
                GenerateDoor(j, RoomList[i].RoomLocation);
            }
            else
            {

            }
        }

    }
    void FixedUpdate()
    {
        if ((spawned) && (GameObject.FindGameObjectWithTag("Enemy") == null))
        {
            Door[] Doors = GetComponentsInChildren<Door>();
            if (this.roomType == RoomType.BossRoom)
            {
                transform.GetComponentInChildren<Exit>().gameObject.SetActive(true);
            }
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].GetComponentInChildren<Collider2D>().enabled = false;
            }
            if (rewarded == false)
            {
                if(roomType == RoomType.AudioRoom)
                {
                    GenerateTreasures();
                }
                else
                {
                    GenerateRewards();
                }
                rewarded = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CamPos = Camera.main.transform.position;
        RoomPos = new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
        if (collision.GetComponent<PlayerControl>() != null)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(CamPos, RoomPos, ref cameraVector3, 0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((spawned == false) && (collision.GetComponent<PlayerControl>() != null))
        {
            Door[] Doors = GetComponentsInChildren<Door>();

            if (spawned == false)
            {
                Vector3 pushVector = new Vector3(collision.GetComponent<Rigidbody2D>().velocity.x, collision.GetComponent<Rigidbody2D>().velocity.y, 0);
                collision.transform.position += pushVector.normalized * 0.75f;
                GenerateEnemies();
                spawned = true;
            }

            for (int i = 0; i < Doors.Length; i++)
            {
                switch(roomType)
                {
                    case RoomType.StartRoom:
                        switch (Doors[i].gameObject.GetComponent<Door>().DoorDirection)
                        {
                            case Door.doorDirection.down:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Green_Idle_Down");
                                break;
                            case Door.doorDirection.left:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Green_Idle_Left");
                                break;
                            case Door.doorDirection.right:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Green_Idle_Right");
                                break;
                            case Door.doorDirection.up:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Green_Idle_Up");
                                break;
                        }
                        break;
                    default:
                        switch (Doors[i].gameObject.GetComponent<Door>().DoorDirection)
                        {
                            case Door.doorDirection.down:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Disappear_Down");
                                break;
                            case Door.doorDirection.left:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Disappear_Left");
                                break;
                            case Door.doorDirection.right:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Disappear_Right");
                                break;
                            case Door.doorDirection.up:
                                Doors[i].gameObject.GetComponent<Animator>().Play("Disappear_Up");
                                break;
                        }
                        break;
                }

                switch(Doors[i].gameObject.GetComponent<Door>().DoorDirection)
                {
                    case Door.doorDirection.down:

                        break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FiredBullet>() != null || collision.GetComponent<ParriedBullet>() != null || collision.GetComponent<EnemyBullet>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
    Vector3 ChangePos(int direction, Vector3 Location)
    {
        direction = Mathf.Clamp(direction, 0, 3);

        Vector3 changedPos = Location;

        switch ((Direction)direction)
        {
            case Direction.up:
                changedPos = Location + new Vector3(0, yOffset, 0);
                return changedPos;
            case Direction.down:
                changedPos = Location + new Vector3(0, -yOffset, 0);
                return changedPos;
            case Direction.left:
                changedPos = Location + new Vector3(-xOffset, 0, 0);
                return changedPos;
            case Direction.right:
                changedPos = Location + new Vector3(xOffset, 0, 0);
                return changedPos;
            default:
                return changedPos;
        }
    }
    void GenerateDoor(int GenerateRoomType, Vector3 generatePoint)
    {
        GameObject Door = Instantiate(DoorObject, generatePoint, Quaternion.identity);
        Door.GetComponent<RoomController>().roomType = (RoomController.RoomType)GenerateRoomType;
        Door.transform.parent = transform;
    }
    public void GenerateEnemies()
    {
        int rand = Random.Range(1, spawnNum + 1);

        for (int i = 0; i < rand; i++)
        {
            int type = Random.Range(1, 4);

            if (roomType == (RoomType)1)
            {
                switch (type)
                {
                    case 1:
                        Instantiate(level_Manager.Enemy_1, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(level_Manager.Enemy_2, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(level_Manager.Enemy_3, gameObject.transform.position, Quaternion.identity);
                        break;
                    default:
                        Instantiate(level_Manager.Enemy_1, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
            else if (roomType == (RoomType)4)
            {
                switch (type)
                {
                    case 1:
                        Instantiate(level_Manager.Boss_1, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(level_Manager.Boss_2, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(level_Manager.Boss_3, gameObject.transform.position, Quaternion.identity);
                        break;
                    default:
                        Instantiate(level_Manager.Boss_1, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }
    public void GenerateRewards()
    {
        int randRewardNum = Random.Range(0, rewardNum + 1);
        for (int i = 0; i < randRewardNum; i++)
        {
            int type = Random.Range(1, 4);

            if (roomType == RoomType.AudioRoom || roomType == RoomType.BossRoom)
            {
                switch (type)
                {
                    case 1:
                        Instantiate(level_Manager.Prop_1, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(level_Manager.Prop_2, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(level_Manager.Prop_3, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case 1:
                        Instantiate(level_Manager.Heal, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(level_Manager.Coin, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(level_Manager.Key, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }
    public void GenerateTreasures()
    {
        for (int i = 0; i < rewardNum; i++)
        {
            int type = Random.Range(1, 4);

            if (roomType == RoomType.AudioRoom || roomType == RoomType.BossRoom)
            {
                switch (type)
                {
                    case 1:
                        Instantiate(Prop_1, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Prop_2, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Prop_3, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case 1:
                        Instantiate(Reward_1, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Reward_2, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Reward_3, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }
    public void OpenTheGay()
    {
        LockedDoor[] LockedDoors = transform.GetComponentsInChildren<LockedDoor>();
        for (int i = 0; i < LockedDoors.Length; i++)
        {
            LockedDoors[i].GetComponent<BoxCollider2D>().enabled = false;
            LockedDoors[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}