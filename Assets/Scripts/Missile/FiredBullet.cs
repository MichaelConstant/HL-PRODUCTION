using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiredBullet : Bullet
{
    private Vector2 BulletVector;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        BulletVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 2)) - gameObject.transform.position);
        GetComponent<Rigidbody2D>().AddForce(BulletVector.normalized * BulletSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.tag) == "Terrain") || (collision.gameObject.GetComponent<BossControl>() != null))
        {
            if ((collision.gameObject.GetComponent<BossControl>() != null))
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
            }
            Destroy(gameObject);
        }
    }
}
