using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    Vector3 CamPos;
    Vector3 RoomPos;
    private Vector3 cameraVector3 = Vector3.zero;

    bool spawned;
    public int spawnNum;

    public GameObject Enemy_1;
    public GameObject Enemy_2;
    public GameObject Enemy_3;

    bool rewarded;
    public int rewardNum;

    public GameObject Reward_1;
    public GameObject Reward_2;
    public GameObject Reward_3;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name != "Room_Start(Clone)")
        {
            spawned = false;
            rewarded = false;
        }
        else
        {
            spawned = true;
            rewarded = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CamPos = Camera.main.transform.position;
        RoomPos = new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
        if (collision.GetComponent<PlayerControl>()!=null)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(CamPos, RoomPos, ref cameraVector3, 0.1f);
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                Door[] Doors = GetComponentsInChildren<Door>();
                for (int i = 0; i < Doors.Length; i++)
                {
                    Doors[i].GetComponentInChildren<Collider2D>().enabled = false;
                }
                if (rewarded == false)
                {
                    GenerateRewards();
                    rewarded = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(spawned == false)
        {
            if (collision.GetComponent<PlayerControl>() != null)
            {
                Vector3 pushVector = new Vector3(collision.GetComponent<Rigidbody2D>().velocity.x, collision.GetComponent<Rigidbody2D>().velocity.y, 0);
                collision.transform.position += pushVector.normalized*0.75f;
                GenerateEnemies();
                spawned = true;
                Door[] Doors = GetComponentsInChildren<Door>();
                for (int i = 0; i < Doors.Length; i++)
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
            int type = Random.Range(1, 4);
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
                default:
                    Instantiate(Enemy_1, gameObject.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
    public void GenerateRewards()
    {
        for (int i = 0; i < rewardNum; i++)
        {
            int type = Random.Range(1, 4);
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