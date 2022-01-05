using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    public class Room
    {
        public int RoomType;
        //0:初始房间，实际不使用
        //1:Room_Common,普通房
        //2:Room_Treasure,宝藏房
        //3:Room_Boss,Boss房
        public Vector3 RoomLocation;
        public Room(int RoomType, Vector3 RoomLocation)
        {
            this.RoomType = RoomType;
            this.RoomLocation = RoomLocation;
        }
    }

    [Header("房间信息")]
    public Transform LevelManager;

    public GameObject Room_Start;
    public GameObject Room_Common;
    public GameObject Room_Treasure;
    public GameObject Room_Boss;

    public GameObject Door_Up;
    public GameObject Door_Down;
    public GameObject Door_Left;
    public GameObject Door_Right;

    public GameObject Block_Up;
    public GameObject Block_Down;
    public GameObject Block_Left;
    public GameObject Block_Right;

    public int RoomNum;

    [Header("位置控制")]
    public Transform GeneratePoint;
    public float xOffset;
    public float yOffset;
    public LayerMask RoomLayer;

    public List<Room> RoomList = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        #region 生成房间列表 Generate Room List
        RoomList.Add(new Room(0, GeneratePoint.position));
        RandomChangePos();

        for (int i=0;i<RoomNum-1;i++)
        {
            var tempPos= GeneratePoint.position;

            if(RoomList.Exists(Room=>Room.RoomLocation == GeneratePoint.position))
            {
                i--;
                GeneratePoint.position = tempPos;
            }
            else
            {
                RoomList.Add(new Room(1, GeneratePoint.position));
            }

            RandomChangePos();

        }
        #endregion
        #region BOSS房判定 Define Boss Room
        //Room endRoom = RoomList[RoomNum-1];
        //foreach (var room in RoomList)
        //{
        //    if (room.RoomLocation.sqrMagnitude > endRoom.RoomLocation.sqrMagnitude)
        //    {
        //        endRoom = room;
        //    }
        //}
        //endRoom.RoomType = 3;
        RoomList[RoomNum - 1].RoomType = 3;
        #endregion
        #region 生成房间和门
        for (int i=0;i<RoomNum;i++)
        {
            if (RoomList[i].RoomType != 0 && RoomList[i].RoomType != 3)
            {
                RoomList[i].RoomType = (Random.Range(1, 3));
            }

            if(i!=0)
            {
                GameObject.FindWithTag("LastSpawn").tag = "Spawned";
            }

            GenerateRoom(RoomList[i].RoomType, RoomList[i].RoomLocation);

            for (int j=0;j<4;j++)
            {
                if (RoomList.Exists(Room => Room.RoomLocation == ChangePos(j, RoomList[i].RoomLocation)))
                {
                    GenerateDoor(j, RoomList[i].RoomLocation);
                }
                else
                {
                    GenerateBlock(j, RoomList[i].RoomLocation);
                }
            }
        }
        #endregion
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 ChangePos(int direction,Vector3 Location)
    {
        direction=Mathf.Clamp(direction, 0, 3);
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
    public void RandomChangePos()
    {
        direction = (Direction)Random.Range(0, 4);
        
        switch (direction)
        {
            case Direction.up:
                GeneratePoint.position += new Vector3(0, yOffset, 0);
                break;
            case Direction.down:
                GeneratePoint.position += new Vector3(0, -yOffset, 0);
                break;
            case Direction.left:
                GeneratePoint.position += new Vector3(-xOffset, 0, 0);
                break;
            case Direction.right:
                GeneratePoint.position += new Vector3(xOffset, 0, 0);
                break;
        }
    }
    public void GenerateRoom(int roomType, Vector3 generatePoint)
    {
        switch (roomType)
        {
            case 0:
                GameObject RoomStart = Instantiate(Room_Start, generatePoint, Quaternion.identity);
                RoomStart.transform.parent = LevelManager.transform;
                RoomStart.tag = "LastSpawn";
                break;
            case 1:
                GameObject RoomCommon = Instantiate(Room_Common, generatePoint, Quaternion.identity);
                RoomCommon.transform.parent = LevelManager.transform;
                RoomCommon.tag = "LastSpawn";
                break;
            case 2:
                GameObject RoomTreasure = Instantiate(Room_Treasure, generatePoint, Quaternion.identity);
                RoomTreasure.transform.parent = LevelManager.transform;
                RoomTreasure.tag = "LastSpawn";
                break;
            case 3:
                GameObject RoomBoss = Instantiate(Room_Boss, generatePoint, Quaternion.identity);
                RoomBoss.transform.parent = LevelManager.transform;
                RoomBoss.tag = "LastSpawn";
                break;
        }
    }
    public void GenerateDoor(int doorDirection, Vector3 generatePoint)
    {
        switch ((Direction)doorDirection)
        {
            case Direction.up:
                GameObject DoorUp = Instantiate(Door_Up, generatePoint, Quaternion.identity);
                DoorUp.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
            case Direction.down:
                GameObject DoorDown = Instantiate(Door_Down, generatePoint, Quaternion.identity);
                DoorDown.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
            case Direction.left:
                GameObject DoorLeft = Instantiate(Door_Left, generatePoint, Quaternion.identity);
                DoorLeft.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
            case Direction.right:
                GameObject DoorRight = Instantiate(Door_Right, generatePoint, Quaternion.identity);
                DoorRight.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
        }
    }
    public void GenerateBlock(int blockDirection, Vector3 generatePoint)
    {
        switch ((Direction)blockDirection)
        {
            case Direction.up:
                GameObject BlockUp = Instantiate(Block_Up, generatePoint, Quaternion.identity);
                BlockUp.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
            case Direction.down:
                GameObject BlockDown = Instantiate(Block_Down, generatePoint, Quaternion.identity);
                BlockDown.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
            case Direction.left:
                GameObject BlockLeft = Instantiate(Block_Left, generatePoint, Quaternion.identity);
                BlockLeft.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
            case Direction.right:
                GameObject BlockRight = Instantiate(Block_Right, generatePoint, Quaternion.identity);
                BlockRight.transform.parent = GameObject.FindWithTag("LastSpawn").transform;
                break;
        }
    }
}