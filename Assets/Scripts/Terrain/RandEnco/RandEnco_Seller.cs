using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEnco_Seller : MonoBehaviour
{
    int randGoodType;
    int goodsIndex;
    int goodsPrice;
    GameObject Goods;
    void Start()
    {
        randGoodType = Random.Range(0, 4);
        if (randGoodType == 0)
        {
            goodsIndex = Random.Range(0, LevelManager.PropsList.Count);
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LevelManager.PropsList[goodsIndex].GetComponent<SpriteRenderer>().sprite;
            Goods = LevelManager.PropsList[goodsIndex];
            goodsPrice = LevelManager.PropsList[goodsIndex].GetComponent<PropBase>().propPrice;
            LevelManager.PropsList.Remove(LevelManager.PropsList[goodsIndex]);
        }
        else
        {
            goodsIndex = Random.Range(0, LevelManager.ItemsList.Count);
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LevelManager.ItemsList[goodsIndex].GetComponent<SpriteRenderer>().sprite;
            goodsPrice = LevelManager.ItemsList[goodsIndex].GetComponent<PropBase>().propPrice;
            Goods = LevelManager.ItemsList[goodsIndex];
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        int sellingPrice = goodsPrice;
        PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();

        if (collision.gameObject.GetComponent<PlayerControl>() != null && transform.GetChild(1).gameObject != null)
        {
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = LevelManager.NumsSprites[sellingPrice];
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (collision.gameObject.GetComponent<PlayerControl>() != null && Input.GetKey(KeyCode.Q) && transform.GetChild(1).gameObject != null)
        {
            if (Player.CoinCounts >= sellingPrice)
            {
                Player.CoinCounts -= sellingPrice;
                Instantiate(Goods, transform.position, Quaternion.identity);
                Destroy(transform.GetChild(0).gameObject);
                Destroy(transform.GetChild(1).gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null && transform.GetChild(0).gameObject != null)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}