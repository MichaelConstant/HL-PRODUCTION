using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public float movementSpeed;

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
        rb.velocity = new Vector2(directionX * movementSpeed, directionY * movementSpeed);
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
    protected void PlayerShoot()
    {
        if (canShoot)
        {
            canShoot = false;
            switch (PlayerControl.MeleeLevelStatic)
            {
                case 0:
                    Instantiate(bullet0, bulletSpawn.transform.position, transform.rotation);
                    break;
                case 1:
                    Instantiate(bullet1, bulletSpawn.transform.position, transform.rotation);
                    break;
                case 2:
                    Instantiate(bullet2, bulletSpawn.transform.position, transform.rotation);
                    break;
                case 3:
                    Instantiate(bullet3, bulletSpawn.transform.position, transform.rotation);
                    break;
            }
            StartCoroutine(ShootInterval());
        }
    }
    protected void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
            GetComponentInChildren<Animator>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            GetComponentInChildren<Animator>().transform.Rotate(0, 0, angle_360(AttackVector));
            anim.Play("Attack");
            StartCoroutine(AttackInterval());
        }
    }

    private IEnumerator ShootInterval()
    {
        yield return new WaitForSeconds(BasicShootInterval);
        canShoot = true;
    }
    private IEnumerator AttackInterval()
    {
        yield return new WaitForSeconds(BasicAttackInterval);
        canAttack = true;
    }
    private float angle_360(Vector2 Vector)
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