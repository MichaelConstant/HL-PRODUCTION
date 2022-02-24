using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    SpriteRenderer sr;

    public Sprite Art;
    public Sprite Design;
    public Sprite Program;
    public Sprite Base;

    public enum RoomType { StartRoom, ProgramRoom, ArtRoom, DesignRoom, BossRoom, RandEncoRoom, None };
    //0,1,2,3,4,5,*6*
    public RoomType roomType;

    enum Direction { up, down, left, right };
    public GameObject DoorObject;

    Vector3 CamPos;
    Vector3 RoomPos;
    Vector3 cameraVector3 = Vector3.zero;

    public bool spawned;
    public int spawnNum;

    public bool rewarded;
    public int rewardNum;

    bool doorAnimPlayed;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        switch (roomType)
        {
            case RoomType.StartRoom:
                sr.sprite = Design;
                sr.color = new Color32(255, 255, 0, 255);
                spawnNum = 0;
                rewardNum = 0;
                break;
            case RoomType.ArtRoom:
                sr.sprite = Art;
                sr.color = new Color32(255, 255, 0, 255);
                break;
            case RoomType.DesignRoom:
                sr.sprite = Design;
                break;
            case RoomType.ProgramRoom:
                sr.sprite = Program;
                break;
            case RoomType.BossRoom:
                sr.sprite = Base;
                spawnNum = 1;
                rewardNum = 1;
                break;
            case RoomType.RandEncoRoom:
                sr.sprite = Base;
                sr.color = new Color32(0, 255, 255, 255);
                spawnNum = 0;
                rewardNum = 0;
                break;
        }

        if (roomType == RoomType.StartRoom)
        {
            spawned = true;
            rewarded = true;
            RoomGenerator.RoomList[0].RoomEntered = true;
        }
        else
        {
            spawned = false;
            rewarded = false;
            doorAnimPlayed = false;
        }

        for (int i = 0; i < 4; i++)
        {
            if (RoomGenerator.RoomList.Exists(Room => Room.RoomLocation == ChangePos(i, transform.position)))
            {
                int RoomTypeAside = RoomGenerator.RoomList.Find(Room => Room.RoomLocation == ChangePos(i, transform.position)).RoomType;
                GenerateDoor(RoomTypeAside, i, transform.position);
            }
            else
            {
                GenerateDoor(6, i, transform.position);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Door[] Doors = GetComponentsInChildren<Door>();

        if (collision.GetComponent<PlayerControl>() != null)
        {
            Vector3 pushVector = new Vector3(collision.GetComponent<Rigidbody2D>().velocity.x, collision.GetComponent<Rigidbody2D>().velocity.y, 0);
            collision.transform.position += pushVector.normalized * 0.8f;
        }

        if ((!spawned) && (collision.GetComponent<PlayerControl>() != null))
        {
            if (roomType == RoomType.RandEncoRoom)
            {
                GameObject RandEncoObject = Instantiate(LevelManager.RandEnco, transform.position, Quaternion.identity);
                RandEncoObject.transform.parent = transform;
            }
            else
            {
                GenerateEnemies();
            }

            spawned = true;
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].ShutTheGay();
            }
            RoomGenerator.RoomList.Find(Room => Room.RoomLocation == transform.position).RoomEntered = true;
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

        if (spawned && GameObject.FindGameObjectWithTag("Enemy") == null && !rewarded)
        {
            GenerateRewards();
            rewarded = true;
        }

        if (!doorAnimPlayed && rewarded)
        {
            Door[] Doors = GetComponentsInChildren<Door>();
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].TurnOnTheGay();
            }
            doorAnimPlayed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FiredBullet>() != null || collision.GetComponent<ParriedBullet>() != null || collision.GetComponent<EnemyBullet>() != null)
        {
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<PlayerControl>() != null)
        {
            doorAnimPlayed = false;
        }
    }
    public Vector3 ChangePos(int direction, Vector3 Location)
    {
        direction = Mathf.Clamp(direction, 0, 3);

        Vector3 changedPos = Location;

        switch ((Direction)direction)
        {
            case Direction.up:
                changedPos = Location + new Vector3(0, GetComponentInParent<RoomGenerator>().yOffset, 0);
                return changedPos;
            case Direction.down:
                changedPos = Location + new Vector3(0, -GetComponentInParent<RoomGenerator>().yOffset, 0);
                return changedPos;
            case Direction.left:
                changedPos = Location + new Vector3(-GetComponentInParent<RoomGenerator>().xOffset, 0, 0);
                return changedPos;
            case Direction.right:
                changedPos = Location + new Vector3(GetComponentInParent<RoomGenerator>().xOffset, 0, 0);
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
    void GenerateEnemies()
    {
        if (roomType == RoomType.ProgramRoom || roomType == RoomType.DesignRoom)
        {
            int randProgramSetType = Random.Range(0, LevelManager.ProgramRoomSetsList.Count);
            GameObject randProgramSet = Instantiate(LevelManager.ProgramRoomSetsList[randProgramSetType], transform.position, Quaternion.identity);
            randProgramSet.transform.parent = transform;
        }
        else if (roomType == RoomType.BossRoom)
        {
            int randBossType = Random.Range(0, LevelManager.BossesList.Count);
            GameObject randBoss = Instantiate(LevelManager.BossesList[randBossType], transform.position, Quaternion.identity);
            LevelManager.BossesList.Remove(randBoss);
        }
    }
    void GenerateRewards()
    {
        if (roomType == RoomType.ArtRoom || roomType == RoomType.BossRoom)
        {
            int type = Random.Range(0, LevelManager.PropsList.Count);
            Instantiate(LevelManager.PropsList[type], gameObject.transform.position, Quaternion.identity);
            LevelManager.PropsList.Remove(LevelManager.PropsList[type]);
        }
        else if (roomType == RoomType.DesignRoom || roomType == RoomType.ProgramRoom)
        {
            int type = Random.Range(0, LevelManager.ItemsList.Count);
            Instantiate(LevelManager.ItemsList[type], gameObject.transform.position, Quaternion.identity);
        }
    }
}