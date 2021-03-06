using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    [HideInInspector]
    public float movementSpeed_Final;
    public float movementSpeed_Basic;
    public float movementSpeed_Ratio;

    [HideInInspector]
    public float meleeDamage_Final;
    public float meleeDamage_Basic;
    public float meleeDamage_Ratio;

    [HideInInspector]
    public float rangeDamage_Final;
    public float rangeDamage_Basic;
    public float rangeDamage_Ratio;

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
    public bool canShoot = true;
    [HideInInspector]
    public bool canAttack = true;
    [HideInInspector]
    public float shootTimer;
    [HideInInspector]
    public float attackTimer;

    [HideInInspector]
    public Shooter_Base Shooter;
    [HideInInspector]
    public RageShooter_Base RageShooter;
    [HideInInspector]
    public UltraShooter_Base UltraShooter;

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
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Shooter = GetComponentInChildren<Shooter_Base>();
        RageShooter = GetComponentInChildren<RageShooter_Base>();
        UltraShooter = GetComponentInChildren<UltraShooter_Base>();

        canShoot = true;
        canAttack = true;

        currentHP = maxHP;
        movementSpeed_Final = movementSpeed_Basic * movementSpeed_Ratio;
        rangeDamage_Final = rangeDamage_Basic * rangeDamage_Ratio;
        meleeDamage_Final = meleeDamage_Basic * meleeDamage_Ratio;
        ShootInterval_Final = ShootInterval_Basic / ShootInterval_Ratio;
        AttackInterval_Final = AttackInterval_Basic / AttackInterval_Ratio;
    }
    public virtual void FixedUpdate()
    {
        if (!canShoot)
        {
            if (shootTimer < ShootInterval_Final)
            {
                shootTimer += Time.deltaTime;
            }
            else
            {
                shootTimer = 0;
                canShoot = true;
            }
        }

        if (!canAttack)
        {
            if (attackTimer < AttackInterval_Final)
            {
                attackTimer += Time.deltaTime;
            }
            else
            {
                attackTimer = 0;
                canAttack = true;
            }
        }
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
        }
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
}