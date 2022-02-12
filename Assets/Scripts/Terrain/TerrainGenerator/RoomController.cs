using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    LevelManager level_Manager;
    RoomGenerator room_Generator;
    SpriteRenderer sr;

    public Sprite Art;
    public Sprite Design;
    public Sprite Program;

    public enum RoomType { StartRoom, ProgramRoom, ArtRoom, DesignRoom, BossRoom, RandEncoRoom };
    //0,1,2,3,4,5
    public RoomType roomType;

    enum Direction { up, down, left, right };
    public GameObject DoorObject;
    Door[] Doors;

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
        Door[] Doors = GetComponentsInChildren<Door>();

        switch (roomType)
        {
            case RoomType.StartRoom:
                sr.sprite = Design;
                sr.color = new Color32(255, 255, 0, 255);
                break;
            case RoomType.ArtRoom:
                sr.sprite = Art;
                break;
            case RoomType.DesignRoom:
                sr.sprite = Design;
                break;
            case RoomType.ProgramRoom:
                sr.sprite = Program;
                break;
            case RoomType.BossRoom:
                sr.sprite = Design;
                sr.color = new Color32(255, 0, 0, 255);
                break;
            case RoomType.RandEncoRoom:
                sr.sprite = Design;
                sr.color = new Color32(0, 255, 255, 255);
                break;
        }
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
                int RoomTypeAside = RoomGenerator.RoomList.Find(Room => Room.RoomLocation == ChangePos(i, transform.position)).RoomType;
                GenerateDoor(RoomTypeAside, i, transform.position);
            }
        }
    }
    void FixedUpdate()
    {
        if ((spawned) && (GameObject.FindGameObjectWithTag("Enemy") == null))
        {
            if (rewarded == false)
            {
                if (roomType == RoomType.ArtRoom)
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
            Vector3 pushVector = new Vector3(collision.GetComponent<Rigidbody2D>().velocity.x, collision.GetComponent<Rigidbody2D>().velocity.y, 0);
            collision.transform.position += pushVector.normalized;
            GenerateEnemies();
            spawned = true;
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
                changedPos = Location + new Vector3(0, room_Generator.yOffset, 0);
                return changedPos;
            case Direction.down:
                changedPos = Location + new Vector3(0, -room_Generator.yOffset, 0);
                return changedPos;
            case Direction.left:
                changedPos = Location + new Vector3(-room_Generator.xOffset, 0, 0);
                return changedPos;
            case Direction.right:
                changedPos = Location + new Vector3(room_Generator.xOffset, 0, 0);
                return changedPos;
            default:
                return changedPos;
        }
    }
    void GenerateDoor(int RoomTypeAside, int DoorDirection,Vector3 generatePoint)
    {
        GameObject GeneratedDoor = Instantiate(DoorObject, generatePoint, Quaternion.identity);
        GeneratedDoor.GetComponent<Door>().roomTypeAside = (Door.RoomTypeAside) RoomTypeAside;
        GeneratedDoor.GetComponent<Door>().doorDirection = (Door.DoorDirection) DoorDirection;
        GeneratedDoor.transform.parent = transform;
    }
    public void GenerateEnemies()
    {
        int rand = Random.Range(1, spawnNum + 1);

        for (int i = 0; i < rand; i++)
        {
            int type = Random.Range(1, 4);

            if (roomType == RoomType.ProgramRoom)
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
            else if (roomType == RoomType.BossRoom)
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

            if (roomType == RoomType.ArtRoom || roomType == RoomType.BossRoom)
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

            if (roomType == RoomType.ArtRoom || roomType == RoomType.BossRoom)
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
    public void OpenTheGay()
    {
        LockedDoor[] LockedDoors = transform.GetComponentsInChildren<LockedDoor>();
        for (int i = 0; i < LockedDoors.Length; i++)
        {
            LockedDoors[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}