using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Sprite num_0;
    public Sprite num_1;
    public Sprite num_2;
    public Sprite num_3;
    public Sprite num_4;
    public Sprite num_5;
    public Sprite num_6;
    public Sprite num_7;
    public Sprite num_8;
    public Sprite num_9;

    GameObject[] Enemies;
    GameObject[] Bosses;
    GameObject[] Items;
    GameObject[] Props;

    GameObject[] ProgramRoomSets;

    public static GameObject RandEnco;

    public Sprite[] Nums;

    public static List<GameObject> EnemiesList = new List<GameObject>();
    public static List<GameObject> BossesList = new List<GameObject>();
    public static List<GameObject> ItemsList = new List<GameObject>();
    public static List<GameObject> PropsList = new List<GameObject>();
    public static List<Sprite> NumsSprites = new List<Sprite>();

    public static List<GameObject> ProgramRoomSetsList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        Sprite[] Nums = new Sprite[] { num_0, num_1, num_2, num_3, num_4, num_5, num_6, num_7, num_8, num_9 };

        Enemies = Resources.LoadAll<GameObject>("Enemy/Common");
        Bosses = Resources.LoadAll<GameObject>("Enemy/Boss");
        Items = Resources.LoadAll<GameObject>("Item");
        Props = Resources.LoadAll<GameObject>("Prop");

        RandEnco = Resources.Load<GameObject>("Terrain/RandEnco/RandEnco");

        ProgramRoomSets = Resources.LoadAll<GameObject>("Terrain/ProgramRoom");

        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemiesList.Add(Enemies[i]);
        }

        for (int i = 0; i < Bosses.Length; i++)
        {
            BossesList.Add(Bosses[i]);
        }

        for (int i = 0; i < Items.Length; i++)
        {
            ItemsList.Add(Items[i]);
        }

        for (int i = 0; i < Props.Length; i++)
        {
            PropsList.Add(Props[i]);
        }

        for (int i = 0; i < Nums.Length; i++)
        {
            NumsSprites.Add(Nums[i]);
        }

        for (int i = 0; i < ProgramRoomSets.Length; i++)
        {
            ProgramRoomSetsList.Add(ProgramRoomSets[i]);
        }
    }
}