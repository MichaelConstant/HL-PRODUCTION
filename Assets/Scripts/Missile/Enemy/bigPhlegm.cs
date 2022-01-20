using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigPhlegm : EnemyBullet
{
    void Start()
    {
        StartCoroutine(DestroySelf());
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}