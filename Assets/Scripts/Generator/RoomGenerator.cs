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
    public GameObject Room_Start;
    public GameObject Room_Common;
    public GameObject Room_Treasure;
    public GameObject Room_Boss;

    public GameObject Door_Up;
    public GameObject Door_Down;
    public GameObject Door_Left;
    public GameObject Door_Right;

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
        #region 生成房间列表
        for (int i=0;i<RoomNum;i++)
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

        #region 初始房、BOSS房判定
        //Room endRoom = RoomList[RoomNum-1];
        //foreach (var room in RoomList)
        //{
        //    if (room.RoomLocation.sqrMagnitude > endRoom.RoomLocation.sqrMagnitude)
        //    {
        //        endRoom = room;
        //    }
        //}
        //endRoom.RoomType = 3;
        RoomList[0].RoomType = 0;
        RoomList[RoomNum - 1].RoomType = 3;

        #endregion

        #region 生成房间和门
        foreach (var room in RoomList)
        {
            if (room.RoomType != 0 && room.RoomType != 3)
            {
                room.RoomType = (Random.Range(1, 3));
            }

            GenerateRoom(room.RoomType, room.RoomLocation);

            for (int i=0;i<4;i++)
            {
                if (RoomList.Exists(Room => Room.RoomLocation == ChangePos(i, room.RoomLocation)))
                {
                    GenerateDoor(i, room.RoomLocation);
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
        switch ((Direction)direction)
        {
            case Direction.up:
                Location += new Vector3(0, yOffset, 0);
                return Location;
            case Direction.down:
                Location += new Vector3(0, -yOffset, 0);
                return Location;
            case Direction.left:
                Location += new Vector3(-xOffset, 0, 0);
                return Location;
            case Direction.right:
                Location += new Vector3(xOffset, 0, 0);
                return Location;
            default:
                return Location;
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
                Instantiate(Room_Start, generatePoint, Quaternion.identity);
                break;
            case 1:
                Instantiate(Room_Common, generatePoint, Quaternion.identity);
                break;
            case 2:
                Instantiate(Room_Treasure, generatePoint, Quaternion.identity);
                break;
            case 3:
                Instantiate(Room_Boss, generatePoint, Quaternion.identity);
                break;
        }
    }

    public void GenerateDoor(int doorDirection, Vector3 generatePoint)
    {
        switch (doorDirection)
        {
            case 0:
                Instantiate(Door_Up, generatePoint, Quaternion.identity);
                break;
            case 1:
                Instantiate(Door_Down, generatePoint, Quaternion.identity);
                break;
            case 2:
                Instantiate(Door_Left, generatePoint, Quaternion.identity);
                break;
            case 3:
                Instantiate(Door_Right, generatePoint, Quaternion.identity);
                break;
        }
    }
}