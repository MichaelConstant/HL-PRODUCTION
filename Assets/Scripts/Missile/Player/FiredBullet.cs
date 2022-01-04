using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredBullet : Bullet
{
    private Vector2 BulletVector;

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
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        BulletVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
        GetComponent<Rigidbody2D>().AddForce(BulletVector.normalized * BulletSpeed);
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
        if ((collision.gameObject.GetComponent<BossControl>() != null) || (collision.gameObject.tag == "Terrain"))
        {
            if (collision.gameObject.GetComponent<BossControl>() != null)
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
                            PlayerControl.RangeEnergyStatic = PlayerControl.RangeEnergyStatic - PlayerControl.RangeEnergyMaxStatic + PlayerControl.RangeEnergyMaxStatic * 0.05f;
                        }
                    }
                    if (PlayerControl.RangeEnergyStatic >= PlayerControl.RangeEnergyMaxStatic && PlayerControl.RangeLevelStatic >= PlayerControl.RangeLevelMaxStatic)
                    {
                        PlayerControl.RangeEnergyStatic = PlayerControl.RangeEnergyMaxStatic;
                        gameObject.GetComponentInParent<PlayerControl>().RangeUltraText.SetActive(true);
                    }
                }
                collision.gameObject.GetComponent<BossControl>().EnemyHP--;
            }
            Destroy(gameObject);
        }
    }
}