using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    LevelManager level_Manager;
    public enum RoomType { StartRoom, ProgramRoom, ArtRoom, DesignRoom, BossRoom, AudioRoom };
    //0,1,2,3,4,5
    public RoomType roomType;

    Vector3 CamPos;
    Vector3 RoomPos;
    Vector3 cameraVector3 = Vector3.zero;

    bool spawned;
    public int spawnNum;
    GameObject Enemy_1;
    GameObject Enemy_2;
    GameObject Enemy_3;

    GameObject Boss_1;
    GameObject Boss_2;
    GameObject Boss_3;

    bool rewarded;
    public int rewardNum;
    GameObject Reward_1;
    GameObject Reward_2;
    GameObject Reward_3;

    GameObject Prop_1;
    GameObject Prop_2;
    GameObject Prop_3;

    // Start is called before the first frame update
    void Start()
    {
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

        level_Manager = GetComponentInParent<LevelManager>();

        Enemy_1 = level_Manager.Enemy_1;
        Enemy_2 = level_Manager.Enemy_2;
        Enemy_3 = level_Manager.Enemy_3;

        Boss_1 = level_Manager.Boss_1;
        Boss_2 = level_Manager.Boss_2;
        Boss_3 = level_Manager.Boss_3;

        Reward_1 = level_Manager.Heal;
        Reward_2 = level_Manager.Coin;
        Reward_3 = level_Manager.Key;

        Prop_1 = level_Manager.Prop_1;
        Prop_2 = level_Manager.Prop_2;
        Prop_3 = level_Manager.Prop_3;
    }
    void FixedUpdate()
    {
        if ((spawned) && (GameObject.FindGameObjectWithTag("Enemy") == null))
        {
            if(gameObject.name== "Room_Boss(Clone)")
            {
                transform.GetChild(5).gameObject.SetActive(true);
            }
            Door[] Doors = GetComponentsInChildren<Door>();
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
            Vector3 pushVector = new Vector3(collision.GetComponent<Rigidbody2D>().velocity.x, collision.GetComponent<Rigidbody2D>().velocity.y, 0);
            collision.transform.position += pushVector.normalized * 0.75f;
            GenerateEnemies();
            spawned = true;
            Door[] Doors = GetComponentsInChildren<Door>();
            for (int i = 0; i < Doors.Length; i++)
            {
                Doors[i].GetComponentInChildren<Collider2D>().enabled = true;
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
            else if (roomType == (RoomType)4)
            {
                switch (type)
                {
                    case 1:
                        Instantiate(Boss_1, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Boss_2, gameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Boss_3, gameObject.transform.position, Quaternion.identity);
                        break;
                    default:
                        Instantiate(Boss_1, gameObject.transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }
    public void GenerateRewards()
    {
        int rand = Random.Range(0, rewardNum + 1);
        for (int i = 0; i < rand; i++)
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