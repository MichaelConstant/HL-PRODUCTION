using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigPhlegm : EnemyBullet
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyItself", 4f);
    }

    // Update is called once per frame

}