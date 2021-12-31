using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("房间信息")]
    public GameObject Room_0;
    public GameObject Room_1;
    public GameObject Room_2;
    public GameObject Room_Boss;
    public int RoomNum;

    [Header("位置控制")]
    public Transform GeneratePoint;
    public float xOffset;
    public float yOffset;
    public LayerMask RoomLayer;

    public List<GameObject> Rooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<RoomNum;i++)
        {
            Vector3 tempPos = GeneratePoint.position;
            Debug.Log("temp Pos: "+tempPos);
            ChangePos();
            Debug.Log("new Pos: " + GeneratePoint.position);
            if (Physics2D.OverlapCircle(GeneratePoint.position,0.2f, RoomLayer)!=null)
            {
                i--;
                GeneratePoint.position = tempPos;
            }
            else
            {
                Rooms.Add(Instantiate(Room_0, GeneratePoint.position,Quaternion.identity));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePos()
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
}