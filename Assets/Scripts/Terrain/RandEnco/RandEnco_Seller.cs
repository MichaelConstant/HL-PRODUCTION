using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEnco_Seller : MonoBehaviour
{
    int randGoodType;
    int goodsIndex;
    int goodsPrice;
    void Start()
    {
        randGoodType = Random.Range(0, 4);
        if (randGoodType == 0)
        {
            goodsIndex = Random.Range(0, LevelManager.PropsList.Count);
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LevelManager.PropsList[goodsIndex].GetComponent<SpriteRenderer>().sprite;
            goodsPrice = LevelManager.PropsList[goodsIndex].GetComponent<PropBase>().propPrice;
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = LevelManager.Nums[goodsPrice];
            LevelManager.PropsList.Remove(LevelManager.PropsList[goodsIndex]);
        }
        else
        {
            goodsIndex = Random.Range(0, LevelManager.ItemsList.Count);
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = LevelManager.ItemsList[goodsIndex].GetComponent<SpriteRenderer>().sprite;
            goodsPrice = LevelManager.PropsList[goodsIndex].GetComponent<PropBase>().propPrice;
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = LevelManager.Nums[goodsPrice];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null && Input.GetKey(KeyCode.Q))
        {
            PlayerControl Player = collision.gameObject.GetComponent<PlayerControl>();
            if (Player.CoinCounts >= goodsPrice)
            {
                Player.CoinCounts -= goodsPrice;
                if (randGoodType == 0)
                {
                    Instantiate(LevelManager.PropsList[goodsIndex], transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(LevelManager.ItemsList[goodsIndex], transform.position, Quaternion.identity);
                }
                Destroy(transform.GetChild(1).gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}