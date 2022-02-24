using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    public int KeyCounts;
    public int CoinCounts;

    public float KnockBackSeconds_Melee;
    public float KnockBackSeconds_Range;

    public float InvincibleSeconds_Melee;
    public float InvincibleSeconds_Range;
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        
    }
}