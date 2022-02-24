using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    enum Direction { up, down, left, right };
    Direction direction;

    public class Room
    {
        public int RoomType;
        //0:初始房
        //1:程序房(普通打怪房间)
        //2:美术房(宝藏房)
        //3:设计房(陷阱机关房)
        //4:BOSS房
        //5:Random Encounter集会房(宝藏房)
        //此处记得检查与RoomController中的枚举类型保持顺序一致，防止出bug
        public Vector3 RoomLocation;
        public bool RoomEntered;
        public bool RoomLocked;
        public Room(int RoomType, Vector3 RoomLocation, bool LockedOrNot)
        {
            this.RoomType = RoomType;
            this.RoomLocation = RoomLocation;
            this.RoomEntered = false;
            this.RoomLocked = LockedOrNot;
        }
    }

    LevelManager LevelManager;

    [Header("房间权重")]
    public int program_Weight;
    public int randEnco_Weight;
    public int design_Weight;

    public GameObject RoomObject;

    public int RoomNum;

    [Header("位置控制")]
    public float xOffset;
    public float yOffset;

    public string Seed;

    public static List<Room> RoomList = new List<Room>();

    private void Awake()
    {
        LevelManager = GetComponentInChildren<LevelManager>();
        RoomList.Clear();
    }
    void Start()
    {
        #region 生成房间列表 Generate Room List

        //var randTemp = new System.Random(Seed.GetHashCode());
        //var rand = randTemp.Next(4, 7);

        //var randTemp = new System.Random(Seed.GetHashCode()).Next(1, 5);

        //Seed = randTemp.ToString();
        //randTemp = new System.Random(Seed.GetHashCode()).Next(1, 5);

        //Seed = randTemp.ToString();

        int randLengthOfFirstRoomLine = Random.Range(4, 7);

        for (int i = 0; i < RoomNum; i++)
        {
            if (i == 0)
            {
                RoomList.Add(new Room(0, transform.position, false));
                continue;
            }
            else if (i == randLengthOfFirstRoomLine)
            {
                transform.position = new Vector3(0, 0, 0);
            }

            var tempPos = transform.position;

            RandomChangePos();

            if (RoomList.Exists(Room => Room.RoomLocation == transform.position))
            {
                i--;
                transform.position = tempPos;
            }
            else
            {
                if (i == randLengthOfFirstRoomLine-1)
                {
                    RoomList.Add(new Room(4, transform.position, false));
                }
                else if (i == RoomNum - 1)
                {
                    RoomList.Add(new Room(2, transform.position, true));
                }
                else
                {
                    int rand = RandomNum();
                    RoomList.Add(new Room(rand, transform.position, false));
                }
            }
        }

        #endregion

        #region 生成房间

        for (int i = 0; i < RoomList.Count; i++)
        {
            GenerateRoom(RoomList[i].RoomType, RoomList[i].RoomLocation);
        }

        #endregion
    }
    void RandomChangePos()
    {
        direction = (Direction)Random.Range(0, 4);

        switch (direction)
        {
            case Direction.up:
                transform.position += new Vector3(0, yOffset, 0);
                break;
            case Direction.down:
                transform.position += new Vector3(0, -yOffset, 0);
                break;
            case Direction.left:
                transform.position += new Vector3(-xOffset, 0, 0);
                break;
            case Direction.right:
                transform.position += new Vector3(xOffset, 0, 0);
                break;
        }
    }
    void GenerateRoom(int GenerateRoomType, Vector3 generatePoint)
    {
        GameObject Room = Instantiate(RoomObject, generatePoint, Quaternion.identity);
        Room.GetComponent<RoomController>().roomType = (RoomController.RoomType) GenerateRoomType;
        Room.transform.parent = LevelManager.transform;
    }
    int RandomNum()
    {
        int sum_Weight = program_Weight + randEnco_Weight + design_Weight;
        int rand_Weight = Random.Range(1, sum_Weight + 1);
        if (rand_Weight >= 1 && rand_Weight <= program_Weight)
        {
            return 1;
        }
        else if (rand_Weight > program_Weight && rand_Weight <= program_Weight + randEnco_Weight)
        {
            return 5;
        }
        else if (rand_Weight > program_Weight + randEnco_Weight && rand_Weight <= sum_Weight)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }
}