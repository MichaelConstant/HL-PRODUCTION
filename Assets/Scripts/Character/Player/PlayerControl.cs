using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{
    #region 变量

    bool isStudio;

    public int KeyCounts;
    public Text keyText;

    public int CoinCounts;
    public Text coinText;

    public Image HPUI;

    public Image MeleeEnergyUI;
    public Text MeleeLevelUI;
    public GameObject MeleeLevelHint;

    public Image RangeEnergyUI;
    public GameObject RangeUltraText;

    [HideInInspector]
    GameObject Shooter;

    #region 能量系统相关

    public bool onRage = false;

    [Header("远程/近战初始能量，默认为0")]
    public static float RangeEnergy_Static;
    public static float MeleeEnergy_Static;
    private readonly float RangeEnergy = 0;
    private readonly float MeleeEnergy = 0;

    [Header("每一击远程/近战获得能量，近战获得能量，远程减少能量")]
    public static float RangeEnergyPerHitStatic;
    public static float MeleeEnergyPerHitStatic;
    public float RangeEnergyPerHit;
    public float MeleeEnergyPerHit;
    //这里的远程每击减少能量指每次远程射击减少近战能量槽的量
    //远程射击每次命中增长远程能量槽固定为1，通过调整远程最大能量上限调整

    [Header("每0.1秒远程/近战能量衰减")]
    public float RangeEnergyDecreaseAmount;
    public float MeleeEnergyDecreaseAmount;

    private bool canDecrease = true;

    [Header("远程/近战初始等级，默认为0")]
    private readonly int RangeLevel = 0;
    private readonly int MeleeLevel = 0;

    public static int RangeLevel_Static;
    public static int MeleeLevel_Static;

    [Header("远程/近战能量上限")]
    public int RangeEnergyMax;
    public int MeleeEnergyMax;

    public static int RangeEnergyMax_Static;
    public static int MeleeEnergyMax_Static;

    [Header("远程/近战等级上限")]
    public int MeleeLevelMax;
    public int RangeLevelMax;

    public static int RangeLevelMax_Static;
    public static int MeleeLevelMax_Static;

    [Header("能量等级升级时的保底百分比，数值为0-1（0%-100%）")]
    public float RangeEnergyProtectPercent;
    public float MeleeEnergyProtectPercent;

    public static float RangeEnergyProtectPercent_Static;
    public static float MeleeEnergyProtectPercent_Static;

    #endregion

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Shooter = gameObject.GetComponentInChildren<Shooter>().gameObject;

        isAlive = true;

        if (SceneManager.GetActiveScene().name != "Studio")
        {
            isStudio = false;
        }
        else
        {
            isStudio = true;
        }

        keyText.text = "Key: " + KeyCounts;
        coinText.text = "Coin: " + CoinCounts;

        RangeEnergy_Static = RangeEnergy;
        MeleeEnergy_Static = MeleeEnergy;

        RangeEnergyPerHitStatic = RangeEnergyPerHit;
        MeleeEnergyPerHitStatic = MeleeEnergyPerHit;

        RangeEnergyPerHitStatic = RangeEnergyPerHit;
        MeleeEnergyPerHitStatic = MeleeEnergyPerHit;

        RangeLevel_Static = RangeLevel;
        MeleeLevel_Static = MeleeLevel;

        RangeEnergyMax_Static = RangeEnergyMax;
        MeleeEnergyMax_Static = MeleeEnergyMax;

        RangeLevelMax_Static = RangeLevelMax;
        MeleeLevelMax_Static = MeleeLevelMax;

        RangeEnergyProtectPercent_Static = RangeEnergyProtectPercent;
        MeleeEnergyProtectPercent_Static = MeleeEnergyProtectPercent;
    }
    // Update is called once per frame
    void Update()
    {
        MeleeEnergyUI.fillAmount = (float)MeleeEnergy_Static / MeleeEnergyMax_Static;
        RangeEnergyUI.fillAmount = (float)RangeEnergy_Static / RangeEnergyMax_Static;
        HPUI.fillAmount = (float)currentHP / maxHP;

        if (isAlive)
        {
            xInput = (int)Input.GetAxisRaw("Horizontal");
            yInput = (int)Input.GetAxisRaw("Vertical");

            #region Character Animation
            if (xInput != 0 || yInput != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            anim.SetBool("isMoving", isMoving);
            AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
            float Angle = Angle_360(AttackVector);
            if ((Angle <= 0 && Angle >= -45) || (Angle >= 0 && Angle <= 45))
            {
                anim.SetInteger("State", 1);
            }
            else if (Angle > 45 && Angle <= 90)
            {
                anim.SetInteger("State", 2);
            }
            else if (Angle >= -90 && Angle < -45)
            {
                anim.SetInteger("State", 3);
            }
            else if (Angle <= -135 || Angle >= 135)
            {
                anim.SetInteger("State", 4);
            }
            else if (Angle > 90 && Angle < 135)
            {
                anim.SetInteger("State", 5);
            }
            else if (Angle > -135 && Angle < -90)
            {
                anim.SetInteger("State", 6);
            }
            #endregion

            #region Character Input
            if (!isStudio)
            {
                if ((Input.GetMouseButton(0)) && (!Input.GetMouseButtonDown(1)))
                {
                    Shooter.GetComponent<Shooter>().enabled = true;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(Attack());
                }
                if (Input.GetKeyDown(KeyCode.E) && RangeLevel_Static == 1)
                {
                    Shooter.GetComponent<UltraShooter>().enabled = true;
                    RangeLevel_Static = 0;
                    RangeEnergy_Static = 0;
                    RangeUltraText.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Q) && MeleeLevel_Static >= 1)
                {
                    onRage = true;
                    MeleeEnergyDecreaseAmount = 0.1f;
                }
            }
            #endregion
        }
    }
    void FixedUpdate()
    {
        if (isAlive && canMove)
        {
            Move(xInput, yInput);
        }
        if (canDecrease == true)
        {
            StartCoroutine(MeleeEnergyDecrease());
        }
        if (currentHP <= 0)
        {
            isAlive = false;
            isMoving = false;
            canMove = false;
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetInteger("State", 0);
            anim.Play("Dead");
            StartCoroutine(PlayerDead());
        }
    }
    public IEnumerator Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
            GetComponentInChildren<Shooter>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            GetComponentInChildren<Shooter>().transform.Rotate(0, 0, Angle_360(AttackVector));
            GetComponentInChildren<Shooter>().GetComponentInChildren<Animator>().Play("Attack");
            yield return new WaitForSeconds(BasicAttackInterval);
            canAttack = true;
        }
    }
    public IEnumerator MeleeEnergyDecrease()
    {
        canDecrease = false;
        MeleeLevelUI.text = "LV: " + (MeleeLevel_Static + 1);
        MeleeLevelHint.GetComponent<Text>().text = "Pree Q to RAGE: LV." + (MeleeLevel_Static + 1);
        if (MeleeLevel_Static > 0)
        {
            MeleeLevelHint.SetActive(true);
        }
        else
        {
            MeleeLevelHint.SetActive(false);
        }
        if (MeleeEnergy_Static > 0)
        {
            MeleeEnergy_Static -= MeleeEnergyDecreaseAmount;
        }
        if (MeleeEnergy_Static <= 0 && MeleeLevel_Static > 0)
        {
            MeleeLevel_Static -= 1;
            MeleeEnergy_Static += MeleeEnergyMax_Static;
        }
        if (MeleeEnergy_Static <= 0 && MeleeLevel_Static == 0)
        {
            onRage = false;
            MeleeEnergyDecreaseAmount = 0.01f;
        }
        yield return new WaitForSeconds(0.1f);
        canDecrease = true;
    }
    public void MeleeEnergyDecreaseOfShooting()
    {
        if (canShoot && !onRage)
        {
            if (MeleeEnergy_Static > 0)
            {
                MeleeEnergy_Static -= RangeEnergyPerHit;
            }
            if (MeleeEnergy_Static <= 0 && MeleeLevel_Static > 0)
            {
                MeleeLevel_Static -= 1;
                MeleeLevelUI.text = "LV: " + MeleeLevel;
                MeleeEnergy_Static = MeleeEnergyMax_Static;
            }
        }
    }
    public IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Studio");
    }
}