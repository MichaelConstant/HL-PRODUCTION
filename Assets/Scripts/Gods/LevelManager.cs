using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static GameObject Enemy_1_Static;
    public static GameObject Enemy_2_Static;
    public static GameObject Enemy_3_Static;

    public GameObject Enemy_1;
    public GameObject Enemy_2;
    public GameObject Enemy_3;

    public static GameObject Boss_1_Static;
    public static GameObject Boss_2_Static;
    public static GameObject Boss_3_Static;

    public GameObject Boss_1;
    public GameObject Boss_2;
    public GameObject Boss_3;

    public static GameObject Heal_Static;
    public static GameObject Coin_Static;
    public static GameObject Key_Static;

    public GameObject Heal;
    public GameObject Coin;
    public GameObject Key;

    public static GameObject Prop_1_Static;
    public static GameObject Prop_2_Static;
    public static GameObject Prop_3_Static;

    public GameObject Prop1;
    public GameObject Prop2;
    public GameObject Prop3;
    // Start is called before the first frame update
    void Awake()
    {
        Enemy_1_Static = Enemy_1;
        Enemy_2_Static = Enemy_2;
        Enemy_3_Static = Enemy_3;

        Boss_1_Static = Boss_1;
        Boss_2_Static = Boss_2;
        Boss_3_Static = Boss_3;

        Heal_Static = Heal;
        Coin_Static = Coin;
        Key_Static = Key;

        Prop_1_Static = Prop1;
        Prop_2_Static = Prop2;
        Prop_3_Static = Prop3;
    }
}
