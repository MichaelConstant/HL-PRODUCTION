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

    Object[] Enemies;
    Object[] Bosses;
    Object[] Items;
    Object[] Props;

    public static List<Object> EnemiesList;
    public static List<Object> BossesList;
    public static List<Object> ItemsList;
    public static List<Object> PropsList;

    private void Awake()
    {
        Enemies =  Resources.LoadAll("Enemy/Common");
        Bosses = Resources.LoadAll("Enemy/Common");
        Items = Resources.LoadAll("Enemy/Item");
        Props = Resources.LoadAll("Enemy/Prop");

        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemiesList.Add((GameObject)Enemies[i]);
            for (int j = 0; j < EnemiesList.Count; j++)
            {
                Debug.Log(EnemiesList[j]);
            }
        }
        for (int i = 0; i < Bosses.Length; i++)
        {
            BossesList.Add((GameObject)Enemies[i]);
        }
        for (int i = 0; i < Items.Length; i++)
        {
            ItemsList.Add((GameObject)Enemies[i]);
        }
        for (int i = 0; i < Props.Length; i++)
        {
            PropsList.Add((GameObject)Enemies[i]);
        }
    }
}