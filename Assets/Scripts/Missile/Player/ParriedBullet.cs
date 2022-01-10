using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParriedBullet : Bullet
{
    private Vector2 ParriedBulletVector;
    private GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindWithTag("Player");
        ParriedBulletVector = (gameObject.transform.position - Target.transform.position);
        GetComponent<Rigidbody2D>().AddForce(ParriedBulletVector.normalized * BulletSpeed);
        Debug.Log(GetComponent<Rigidbody2D>().velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
