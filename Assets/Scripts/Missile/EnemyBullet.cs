using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector2 BulletVector;
    public float BulletSpeed;
    private GameObject Target;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        BulletVector = (Target.transform.position - gameObject.transform.position);
        GetComponent<Rigidbody2D>().AddForce(BulletVector.normalized * BulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Terrain")||(collision.gameObject.GetComponent<PlayerControl>() != null))
        {
            Destroy(gameObject);
        }
    }
}
