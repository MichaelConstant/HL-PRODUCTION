using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum BulletType { CommonBullet, TerrainBullet };
    public BulletType bulletType;

    public Vector2 BulletVector;

    public int BulletDamage;
    public float BulletSpeed;

    public GameObject Player;
    public virtual void Start()
    {
        Player = GameObject.FindWithTag("Player");
        BulletVector = (Player.transform.position - gameObject.transform.position);
        GetComponent<Rigidbody2D>().AddForce(BulletVector.normalized * BulletSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.GetComponent<PlayerControl>() != null || collision.gameObject.layer == 8)
        {
            if (collision.gameObject.GetComponent<PlayerControl>() != null)
            {
                collision.gameObject.GetComponent<PlayerControl>().currentHP -= BulletDamage;
            }
            switch (bulletType)
            {
                case BulletType.CommonBullet:
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}