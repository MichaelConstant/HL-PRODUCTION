using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float movementSpeed;
    public int maxHP;
    public int currentHP;
    public int meleeDamage;

    public float BasicShootInterval;
    public float BasicAttackInterval;

    [HideInInspector]
    public bool isAlive = true;
    public bool isMoving = false;
    public bool canMove = true;

    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;

    public int xInput;
    public int yInput;

    public bool canShoot;
    public bool canAttack;

    public GameObject Shooter;
    public GameObject bullet_0;
    public GameObject bullet_1;
    public GameObject bullet_2;
    public GameObject bullet_3;
    public GameObject bullet_Ultra;
    public Transform bulletSpawn;

    public Vector2 AttackVector;

    void Awake()
    {
        canShoot = true;
        canAttack = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Shooter = GetComponentInChildren<Shooter>().gameObject;
        currentHP = maxHP;
    }
    public void Move(int directionX, int directionY)
    {
        rb.velocity = (new Vector2(directionX * Time.deltaTime, directionY * Time.deltaTime)).normalized * movementSpeed;
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
        yield return new WaitForSeconds(BasicShootInterval);
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
        Collision.GetComponent<PlayerControl>().currentHP -= meleeDamage;
        Rigidbody2D collisionRB = Collision.GetComponent<Rigidbody2D>();
        collisionRB.velocity = new Vector3(0, 0, 0);
        collisionRB.AddForce((collisionRB.transform.position - gameObject.transform.position).normalized);
        yield return new WaitForSeconds(0.2f);
        Collision.GetComponent<PlayerControl>().canMove = true;
    }
}