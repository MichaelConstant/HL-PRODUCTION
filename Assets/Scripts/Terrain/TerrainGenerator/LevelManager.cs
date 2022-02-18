using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int spawnNumForArt;
    public int rewardNumForArt;

    public int spawnNumForDesign;
    public int rewardNumForDesign;

    public int spawnNumForProgram;
    public int rewardNumForProgram;

    public int spawnNumForBoss;
    public int rewardNumForBoss;

    GameObject[] Enemies;
    GameObject[] Bosses;
    GameObject[] Items;
    GameObject[] Props;

    public static List<GameObject> EnemiesList = new List<GameObject>();
    public static List<GameObject> BossesList = new List<GameObject>();
    public static List<GameObject> ItemsList = new List<GameObject>();
    public static List<GameObject> PropsList = new List<GameObject>();

    private void Awake()
    {
        Enemies = Resources.LoadAll<GameObject>("Enemy/Common");
        Bosses = Resources.LoadAll<GameObject>("Enemy/Boss");
        Items = Resources.LoadAll<GameObject>("Item");
        Props = Resources.LoadAll<GameObject>("Prop");

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