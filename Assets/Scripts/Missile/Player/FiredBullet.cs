using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredBullet : Bullet
{
    [Header("生成残影物体")]
    public GameObject BulletEffect;

    [Header("生成残影的时间间隔")]
    public float spawnTimeval;
    private float spawnTimer;

    private SpriteRenderer sr;

    public Sprite Bullet_0;
    public Sprite Bullet_1;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int BulletType = Random.Range(0, 2);
        if (BulletType == 0)
        {
            sr.sprite = Bullet_0;
        }
        else
        {
            sr.sprite = Bullet_1;
        }
        GetComponent<Rigidbody2D>().AddForce(transform.up * BulletSpeed);
        Destroy(gameObject, BulletLifeSpan);
    }
    // Update is called once per frame
    void Update()
    {
        if (spawnTimer >= spawnTimeval)
        {
            spawnTimer = 0;
            Instantiate(BulletEffect, transform.position, transform.rotation);
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<CharacterControl>() != null) || (collision.gameObject.tag == "Terrain"))
        {
            if (collision.gameObject.GetComponent<CharacterControl>() != null)
            {
                if (gameObject.tag != "Ultra")
                {
                    if (PlayerControl.RangeEnergyStatic < PlayerControl.RangeEnergyMaxStatic)
                    {
                        PlayerControl.RangeEnergyStatic += 1;
                    }
                    if (PlayerControl.RangeEnergyStatic >= PlayerControl.RangeEnergyMaxStatic && PlayerControl.RangeLevelStatic < PlayerControl.RangeLevelMaxStatic)
                    {
                        PlayerControl.RangeLevelStatic += 1;
                        if (PlayerControl.RangeLevelStatic < PlayerControl.RangeLevelMaxStatic)
                        {
                            PlayerControl.RangeEnergyStatic = PlayerControl.RangeEnergyStatic - PlayerControl.RangeEnergyMaxStatic + PlayerControl.RangeEnergyMaxStatic * PlayerControl.MeleeEnergyProtectPercentStatic;
                        }
                    }
                    if (PlayerControl.RangeEnergyStatic >= PlayerControl.RangeEnergyMaxStatic && PlayerControl.RangeLevelStatic >= PlayerControl.RangeLevelMaxStatic)
                    {
                        PlayerControl.RangeEnergyStatic = PlayerControl.RangeEnergyMaxStatic;
                        gameObject.GetComponentInParent<PlayerControl>().RangeUltraText.SetActive(true);
                    }
                }
                collision.gameObject.GetComponent<CharacterControl>().currentHP--;
            }
            Destroy(gameObject);
        }
    }
}