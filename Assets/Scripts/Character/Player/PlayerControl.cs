using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{
    #region 变量

    public int KeyCounts;
    public Text keyText;

    public int CoinCounts;
    public Text coinText;

    [Header("近战/远程受击被击退时间/无敌时间, 单位秒")]
    public float KnockBackSeconds_Melee;
    public float KnockBackSeconds_Range;

    public float InvincibleSeconds_Melee;
    public float InvincibleSeconds_Range;

    public Image MeleeEnergyUI;
    public Text MeleeLevelUI;
    public GameObject MeleeLevelHint;

    public Image RangeEnergyUI;
    public GameObject RangeUltraUI;


    #region 能量系统相关

    [HideInInspector]
    public bool onRage = false;

    [Header("远程/近战初始能量，默认为0")]
    public float RangeEnergy = 0;
    public float MeleeEnergy = 0;

    [Header("每一击远程/近战获得能量，近战获得能量，远程减少能量")]
    public float RangeEnergyPerHit;
    public float MeleeEnergyPerHit;
    //这里的远程每击减少能量指每次远程射击减少近战能量槽的量
    //远程射击每次命中增长远程能量槽固定为1，通过调整远程最大能量上限调整

    float EnergyDecreaseTimer;
    [HideInInspector]
    public float RangeEnergyDecreaseAmountInUse;
    [HideInInspector]
    public float MeleeEnergyDecreaseAmountInUse;

    [Header("通常状态近战、远程能量衰减")]
    public float RangeEnergyDecreaseAmount_Common;
    public float MeleeEnergyDecreaseAmount_Common;

    [Header("Rage状态近战、远程能量衰减")]
    public float RangeEnergyDecreaseAmount_Rage;
    public float MeleeEnergyDecreaseAmount_Rage;

    private bool canDecrease = true;

    [Header("远程/近战初始等级，默认为0")]
    public int RangeLevel = 0;
    public int MeleeLevel = 0;

    [Header("远程/近战能量上限")]
    public int RangeEnergyMax;
    public int MeleeEnergyMax;

    [Header("远程/近战等级上限")]
    public int MeleeLevelMax;
    public int RangeLevelMax;

    [Header("能量等级升级时的保底百分比，数值为0-1（0%-100%）")]
    public float RangeEnergyProtectPercent;
    public float MeleeEnergyProtectPercent;

    #endregion

    #endregion
    void Start()
    {

    }
    void Update()
    {
        keyText.text = ($"{KeyCounts}");
        coinText.text = ($"{CoinCounts}");
        //MeleeEnergyUI.fillAmount = (float)MeleeEnergy / MeleeEnergyMax;
        RangeEnergyUI.fillAmount = (float)RangeEnergy / RangeEnergyMax;

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

            if ( Input.GetMouseButton(0) && !Input.GetMouseButtonDown(1) )
            {
                if (!onRage)
                {
                    Shooter.Fire();
                }
                else
                {
                    RageShooter.Fire();
                }
            }
            if (Input.GetMouseButtonDown(1) && canAttack)
            {
                canAttack = false;
                AttackVector = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -Camera.main.transform.position.z)) - gameObject.transform.position);
                transform.GetChild(2).GetComponent<Animator>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.GetChild(2).GetComponent<Animator>().transform.Rotate(0, 0, Angle_360(AttackVector));
                transform.GetChild(2).GetComponent<Animator>().Play("Attack");
            }
            if (Input.GetKeyDown(KeyCode.E) && RangeLevel >= 1)
            {
                UltraShooter.Fire();
                RangeLevel -= 1;
                RangeEnergy = 0;
                RangeUltraUI.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Space) && MeleeLevel >= 1)
            {
                onRage = true;
                MeleeEnergyDecreaseAmountInUse = 0.1f;
            }
            #endregion
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isAlive && canMove)
        {
            Move(xInput, yInput);
        }

        if (canDecrease)
        {
            canDecrease = false;
            //MeleeLevelUI.text = "LV: " + (MeleeLevel + 1);
            //MeleeLevelHint.GetComponent<Text>().text = "Pree Q to RAGE: LV." + (MeleeLevel + 1);
            if (MeleeLevel > 0)
            {
                //MeleeLevelHint.SetActive(true);
            }
            else
            {
                //MeleeLevelHint.SetActive(false);
            }
            if (MeleeEnergy > 0)
            {
                MeleeEnergy -= MeleeEnergyDecreaseAmountInUse;
            }
            if (MeleeEnergy <= 0 && MeleeLevel > 0)
            {
                MeleeLevel -= 1;
                MeleeEnergy += MeleeEnergyMax;
            }
            if (MeleeEnergy <= 0 && MeleeLevel == 0)
            {
                onRage = false;
                MeleeEnergyDecreaseAmountInUse = 0.01f;
            }
            canDecrease = true;
        }
        else
        {
            if (EnergyDecreaseTimer < 0.1)
            {
                EnergyDecreaseTimer += Time.deltaTime;
            }
            else
            {
                EnergyDecreaseTimer = 0;
                canDecrease = true;
            }
        }

        if (currentHP <= 0)
        {
            if(gameObject.GetComponent<Sakiro_Buff>() != null)
            {
                currentHP = maxHP / 2;
                Destroy(gameObject.GetComponent<Sakiro_Buff>());
            }
            else
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
    }
    public void MeleeEnergyDecreaseOfShooting()
    {
        if (canShoot && !onRage)
        {
            if (MeleeEnergy > 0)
            {
                MeleeEnergy -= RangeEnergyPerHit;
            }
            if (MeleeEnergy <= 0 && MeleeLevel > 0)
            {
                MeleeLevel -= 1;
                MeleeLevelUI.text = "LV: " + MeleeLevel;
                MeleeEnergy = MeleeEnergyMax;
            }
        }
    }
    public IEnumerator PlayerDead()
    {
        RoomGenerator.RoomList.Clear();
        yield return new WaitForSeconds(3f);
        FirstAnimation.isDead = true;
        SceneManager.LoadScene(1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterControl>() != null)
        {
            currentHP -= collision.gameObject.GetComponent<CharacterControl>().meleeDamage_Final;
            canMove = false;
            rb.velocity = new Vector2 (0,0);
            rb.velocity = (transform.position - collision.transform.position).normalized * 2.5f;
            if (gameObject.layer == 6)
            {
                gameObject.layer = 11;
            }
            else
            {
                gameObject.layer = 12;
            }
            StartCoroutine(Invincible(KnockBackSeconds_Melee, InvincibleSeconds_Melee));
            StartCoroutine(ChangeColor(InvincibleSeconds_Melee));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBullet>() != null)
        {
            Debug.Log("111");
            canMove = false;
            rb.velocity = new Vector2(0, 0);
            rb.velocity = (transform.position - collision.transform.position).normalized * 2.5f;
            if (gameObject.layer == 6)
            {
                gameObject.layer = 11;
            }
            else
            {
                gameObject.layer = 12;
            }
            StartCoroutine(Invincible(KnockBackSeconds_Range, InvincibleSeconds_Range));
            StartCoroutine(ChangeColor(InvincibleSeconds_Range));
        }
    }

    IEnumerator Invincible(float KnockBackSeconds, float InvincibleSeconds)
    {
        yield return new WaitForSeconds(KnockBackSeconds);
        canMove = true;
        yield return new WaitForSeconds(InvincibleSeconds);
        if (gameObject.layer == 11)
        {
            gameObject.layer = 6;
        }
        else
        {
            gameObject.layer = 9;
        }
    }
    IEnumerator ChangeColor(float InvincibleSeconds)
    {
        Color Red = new Color32(255, 0, 0, 255);
        Color Common = new Color32(255, 255, 255, 255);
        float t;
        for (int i=0; i< (int)(InvincibleSeconds / 0.2f); i++)
        {
            t = 0f;
            while (t <= 1f)
            {
                t += Time.deltaTime / 0.1f;
                sr.color = Color32.Lerp(Common, Red, t);
                yield return null;
            }
            t = 0f;
            while (t <= 1f)
            {
                t += Time.deltaTime / 0.1f;
                sr.color = Color32.Lerp(Red, Common, t);
                yield return null;
            }
        }
    }
}