using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject mapSpriteBlack;
    GameObject mapSpriteWhite;

    private void OnEnable()
    {
        mapSpriteBlack = transform.parent.GetChild(0).gameObject;
        mapSpriteWhite = transform.parent.GetChild(1).gameObject;
        mapSpriteBlack.SetActive(true);
        mapSpriteWhite.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mapSpriteBlack.SetActive(false);
            mapSpriteWhite.SetActive(true);
        }
    }
}
