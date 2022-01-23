using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum BulletType { CommonBullet, TerrainBullet };
    public BulletType bulletType;

    private Vector2 BulletVector;

    public int BulletDamage;
    public float BulletSpeed;

    private GameObject Player;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        BulletVector = (Player.transform.position - gameObject.transform.position);
        GetComponent<Rigidbody2D>().AddForce(BulletVector.normalized * BulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain" || collision.gameObject.GetComponent<PlayerControl>() != null)
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
