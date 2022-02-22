using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int spawnNumForDesign;

    public int spawnNumForProgram;

    public int spawnNumForBoss;

    public static Sprite Num_0;
    public static Sprite Num_1;
    public static Sprite Num_2;
    public static Sprite Num_3;
    public static Sprite Num_4;
    public static Sprite Num_5;
    public static Sprite Num_6;
    public static Sprite Num_7;
    public static Sprite Num_8;
    public static Sprite Num_9;

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

    public static GameObject RandEnco;

    public static Sprite[] Nums;

    public static List<GameObject> EnemiesList = new List<GameObject>();
    public static List<GameObject> BossesList = new List<GameObject>();
    public static List<GameObject> ItemsList = new List<GameObject>();
    public static List<GameObject> PropsList = new List<GameObject>();

    private void Awake()
    {
        Num_0 = num_0;
        Num_1 = num_1;
        Num_2 = num_2;
        Num_3 = num_3;
        Num_4 = num_4;
        Num_5 = num_5;
        Num_6 = num_6;
        Num_7 = num_7;
        Num_8 = num_8;
        Num_9 = num_9;

        Sprite[] Nums = new Sprite[] { Num_0, Num_1, Num_2, Num_3, Num_4, Num_5, Num_6, Num_7, Num_8, Num_9 };

        for (int i = 0; i < Nums.Length; i++)
        {
            Debug.Log(Nums[i]);
        }

        Enemies = Resources.LoadAll<GameObject>("Enemy/Common");
        Bosses = Resources.LoadAll<GameObject>("Enemy/Boss");
        Items = Resources.LoadAll<GameObject>("Item");
        Props = Resources.LoadAll<GameObject>("Prop");

        RandEnco = Resources.Load<GameObject>("Terrain/RandEnco/RandEnco");

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
    }
}