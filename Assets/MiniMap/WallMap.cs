using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject mapSprite;
    public Color32 color1;
    public Color32 color2;

    private void OnEnable()
    {
        mapSprite = transform.parent.GetChild(0).gameObject;
        mapSprite.GetComponent<SpriteRenderer>().color = color1;
     
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mapSprite.GetComponent<SpriteRenderer>().color = color2;        }
    }
}
