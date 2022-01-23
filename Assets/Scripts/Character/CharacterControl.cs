using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public int maxHP;
    public int currentHP;

    [HideInInspector]
    public float movementSpeed_Final;
    public float movementSpeed_Basic;
    public float movementSpeed_Ratio;

    [HideInInspector]
    public int meleeDamage_Final;
    public int meleeDamage_Basic;
    public int meleeDamage_Ratio;

    [HideInInspector]
    public int rangeDamage_Final;
    public int rangeDamage_Basic;
    public int rangeDamage_Ratio;

    [HideInInspector]
    public float ShootInterval_Final;
    public float ShootInterval_Basic;
    public float ShootInterval_Ratio;

    [HideInInspector]
    public float AttackInterval_Final;
    public float AttackInterval_Basic;
    public float AttackInterval_Ratio;


    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public bool isMoving = false;
    [HideInInspector]
    public bool canMove = true;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SpriteRenderer sr;

    [HideInInspector]
    public int xInput;
    [HideInInspector]
    public int yInput;

    [HideInInspector]
    public bool canShoot;
    [HideInInspector]
    public bool canAttack;

    [HideInInspector]
    public Shooter Shooter;

    public GameObject bullet_0;
    public GameObject bullet_1;
    public GameObject bullet_2;
    public GameObject bullet_3;
    public GameObject bullet_Ultra;
    public Transform bulletSpawn;

    [HideInInspector]
    public Vector2 AttackVector;

    void Awake()
    {
        canShoot = true;
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Shooter = GetComponentInChildren<Shooter>();

        currentHP = maxHP;
        movementSpeed_Final = movementSpeed_Basic * movementSpeed_Ratio;
        rangeDamage_Final = rangeDamage_Basic * rangeDamage_Ratio;
        meleeDamage_Final = meleeDamage_Basic * meleeDamage_Ratio;
        ShootInterval_Final = ShootInterval_Basic / ShootInterval_Ratio;
        AttackInterval_Final = AttackInterval_Basic / AttackInterval_Ratio;
    }
    public void Move(int directionX, int directionY)
    {
        rb.velocity = (new Vector2(directionX * Time.deltaTime, directionY * Time.deltaTime)).normalized * movementSpeed_Final;
    }
    public void CommonShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            Instantiate(bullet_0, bulletSpawn.transform.position, transform.rotation);
            StartCoroutine(ShootInterval());
        }
    }
    public IEnumerator ShootInterval()
    {
        yield return new WaitForSeconds(ShootInterval_Final);
        canShoot = true;
    }
    public float Angle_360(Vector2 Vector)
    {
        float x = Vector.x;
        float y = Vector.y;

        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

        float cos = y / hypotenuse;
        float radian = Mathf.Acos(cos);

        float angle = 180 / (Mathf.PI / radian);

        if (x > 0)
        {
            angle = -angle;
        }
        return angle;
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            StartCoroutine(HitPlayer(collision));
        }
    }
    protected IEnumerator HitPlayer(Collision2D collision)
    {
        GameObject Collision = collision.gameObject;
        Collision.GetComponent<PlayerControl>().canMove = false;
        Collision.GetComponent<PlayerControl>().currentHP -= meleeDamage_Final;
        Rigidbody2D collisionRB = Collision.GetComponent<Rigidbody2D>();
        collisionRB.velocity = new Vector3(0, 0, 0);
        collisionRB.AddForce((collisionRB.transform.position - gameObject.transform.position).normalized);
        yield return new WaitForSeconds(0.2f);
        Collision.GetComponent<PlayerControl>().canMove = true;
    }
}