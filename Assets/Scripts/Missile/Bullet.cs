using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 BulletVector;
    public float BulletSpeed;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
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
            if((collision.gameObject.GetComponent<BossControl>() != null))
            {
                if(PlayerControl.RangeEnergy<10)
                {
                    PlayerControl.RangeEnergy += 1;
                }
                Debug.Log(PlayerControl.RangeEnergy);
            }
            Destroy(gameObject);
        }
    }
}