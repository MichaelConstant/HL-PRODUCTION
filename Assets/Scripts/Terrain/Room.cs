using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    Vector3 CamPos;
    Vector3 RoomPos;
    private Vector3 cameraVector3 = Vector3.zero;

    public GameObject Enemy_1;
    public GameObject Enemy_2;
    public GameObject Enemy_3;

    public int spawnNum;

    public GameObject Reward_1;
    public GameObject Reward_2;
    public GameObject Reward_3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CamPos = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,-2.5f);
        RoomPos = new Vector3(this.transform.position.x, this.transform.position.y, -2.5f);
        if (collision.GetComponent<PlayerControl>()!=null)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(CamPos, RoomPos, ref cameraVector3, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>() != null)
        {
            collision.transform.position += (Vector3)collision.GetComponent<Rigidbody2D>().velocity.normalized * 0.75f;
            if (gameObject.name != "Room_Start(Clone)")
            {
                GenerateEnemies();
                Door[] Doors = GetComponentsInChildren<Door>();
                Debug.Log(Doors.Length);
                for(int i=0;i< Doors.Length;i++)
                {
                    Doors[i].GetComponentInChildren<Collider2D>().enabled = true;
                }
            }
        }
    }

    public void GenerateEnemies()
    {

        for (int i=0;i<spawnNum;i++)
        {
            int type = Random.Range(1, 3);
            switch (type)
            {
                case 1:
                    Instantiate(Enemy_1, gameObject.transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(Enemy_2, gameObject.transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(Enemy_3, gameObject.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}
