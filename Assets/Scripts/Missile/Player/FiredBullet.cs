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
        if ((collision.gameObject.tag == "Enemy") || (collision.gameObject.tag == "Terrain"))
        {
            if (collision.gameObject.tag == "Enemy")
            {
                if (gameObject.tag != "UltraBullet")
                {
                    if (Player.RangeEnergy < Player.RangeEnergyMax)
                    {
                        Player.RangeEnergy += 1;
                    }
                    if (Player.RangeEnergy >= Player.RangeEnergyMax && Player.RangeLevel < Player.RangeLevelMax)
                    {
                        Player.RangeLevel += 1;
                        if (Player.RangeLevel < Player.RangeLevelMax)
                        {
                            Player.RangeEnergy = Player.RangeEnergy - Player.RangeEnergyMax + Player.RangeEnergyMax * Player.MeleeEnergyProtectPercent;
                        }
                    }
                    if (Player.RangeEnergy >= Player.RangeEnergyMax && Player.RangeLevel >= Player.RangeLevelMax)
                    {
                        Player.RangeEnergy = Player.RangeEnergyMax;
                        Player.RangeUltraUI.SetActive(true);
                    }
                }
                collision.gameObject.GetComponent<CharacterControl>().currentHP -= BulletDamage;
            }
            Destroy(gameObject);
        }
    }
}