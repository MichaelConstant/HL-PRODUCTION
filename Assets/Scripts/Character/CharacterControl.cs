using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public float movementSpeed;
    public float HP;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer sr;

    protected float xInput;
    protected float yInput;

    protected bool canShoot;
    protected bool canAttack;

    public float BasicShootInterval;
    public float BasicAttackInterval;
    public GameObject bullet0;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bulletUltra;
    public Transform bulletSpawn;

    protected Vector2 AttackVector;

    void Awake()
    {
        canShoot = true;
        canAttack = true;
        rb =GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim=GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Move(float directionX, float directionY)
    {
        rb.velocity = new Vector2(directionX * movementSpeed * Time.deltaTime, directionY * movementSpeed * Time.deltaTime);
    }
    protected void CommonShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            Instantiate(bullet0, bulletSpawn.transform.position, transform.rotation);
            StartCoroutine(ShootInterval());
        }
        
    }
    protected IEnumerator ShootInterval()
    {
        yield return new WaitForSeconds(BasicShootInterval);
        canShoot = true;
    }
    protected IEnumerator AttackInterval()
    {
        yield return new WaitForSeconds(BasicAttackInterval);
        canAttack = true;
    }
    protected float Angle_360(Vector2 Vector)
    {
        float x = Vector.x;
        float y = Vector.y;

        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

        float cos = y / hypotenuse;
        float radian = Mathf.Acos(cos);

        float angle = 180 / (Mathf.PI / radian);

        if (x>0)
        {
            angle = -angle;
        }
        return angle;
    }
}