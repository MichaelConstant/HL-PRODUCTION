using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
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

        Enemy_1 = LevelManager.Enemy_1_Static;
        Enemy_2 = LevelManager.Enemy_2_Static;
        Enemy_3 = LevelManager.Enemy_3_Static;

        Boss_1 = LevelManager.Boss_1_Static;
        Boss_2 = LevelManager.Boss_2_Static;
        Boss_3 = LevelManager.Boss_3_Static;

        Reward_1 = LevelManager.Heal_Static;
        Reward_2 = LevelManager.Coin_Static;
        Reward_3 = LevelManager.Key_Static;

        Prop_1 = LevelManager.Prop_1_Static;
        Prop_2 = LevelManager.Prop_2_Static;
        Prop_3 = LevelManager.Prop_3_Static;
    }
    void FixedUpdate()
    {
        if ((spawned == true) && (GameObject.FindGameObjectWithTag("Enemy") == null))
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
}