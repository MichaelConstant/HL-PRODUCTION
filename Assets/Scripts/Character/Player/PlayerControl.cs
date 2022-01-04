using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{
	#region VARS
	public Image MeleeEnergyUI;
    public Text MeleeLevelUI;

    public Image RangeEnergyUI;
    public GameObject RangeUltraText;

    public static float RangeEnergyStatic;
    public static float MeleeEnergyStatic;

    [Header("远程/近战初始能量，默认为0")]
    private float RangeEnergy = 0;
    private float MeleeEnergy = 0;

    public static float RangeEnergyPerHitStatic;
    public static float MeleeEnergyPerHitStatic;

    [Header("每一击远程/近战获得能量，近战获得能量，远程减少能量")]
    public float RangeEnergyPerHit;
    public float MeleeEnergyPerHit;
    //这里的远程每击减少能量指每次远程射击减少近战能量槽的量，远程射击每次命中增长远程能量槽固定为1，通过调整远程最大能量上限调整

    [Header("每0.1秒远程/近战能量衰减")]
    public float RangeEnergyDecreaseAmount;
    public float MeleeEnergyDecreaseAmount;

    private bool canDecrease = true;

    public static int RangeLevelStatic;
    public static int MeleeLevelStatic;

    [Header("远程/近战初始等级，默认为0")]
    private int RangeLevel = 0;
    private int MeleeLevel = 0;

    public static int RangeEnergyMaxStatic;
    public static int MeleeEnergyMaxStatic;
    
    [Header("远程/近战能量上限")]
    public int RangeEnergyMax;
    public int MeleeEnergyMax;

    public static int RangeLevelMaxStatic;
    public static int MeleeLevelMaxStatic;

    [Header("远程/近战等级上限")]
    public int MeleeLevelMax;
    public int RangeLevelMax;

    public static float RangeEnergyProtectPercentStatic;
    public static float MeleeEnergyProtectPercentStatic;

    [Header("能量等级升级时的保底百分比，数值为0-1（0%-100%）")]
    public float RangeEnergyProtectPercent;
    public float MeleeEnergyProtectPercent;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        RangeEnergyStatic = RangeEnergy;
        MeleeEnergyStatic = MeleeEnergy;

        RangeEnergyPerHitStatic = RangeEnergyPerHit;
        MeleeEnergyPerHitStatic = MeleeEnergyPerHit;

        RangeEnergyPerHitStatic = RangeEnergyPerHit;
        MeleeEnergyPerHitStatic = MeleeEnergyPerHit;

        RangeLevelStatic = RangeLevel;
        MeleeLevelStatic = MeleeLevel;

        RangeEnergyMaxStatic = RangeEnergyMax;
        MeleeEnergyMaxStatic = MeleeEnergyMax;

        RangeLevelMaxStatic = RangeLevelMax;
        MeleeLevelMaxStatic = MeleeLevelMax;

        RangeEnergyProtectPercentStatic = RangeEnergyProtectPercent;
        MeleeEnergyProtectPercentStatic = MeleeEnergyProtectPercent;
    }
    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Move(xInput, yInput);

        if (Input.GetMouseButton(0))
        {
            PlayerShoot();
            MeleeEnergyDecreaseOfShooting();
        }

        if (Input.GetMouseButton(1))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.E) && RangeLevelStatic==1)
        {
            Instantiate(bulletUltra, bulletSpawn.transform.position, transform.rotation);
            RangeLevelStatic = 0;
            RangeEnergyStatic = 0;
            RangeUltraText.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        MeleeEnergyUI.fillAmount = (float)MeleeEnergyStatic / MeleeEnergyMaxStatic;
        RangeEnergyUI.fillAmount = (float)RangeEnergyStatic / RangeEnergyMaxStatic;

        if (canDecrease == true)
        {
            MeleeEnergyDecrease();
            MeleeLevelUI.text = "LV: " + MeleeLevelStatic;
        }
    }

    private IEnumerator MeleeEnergyDecreaseInterval()
    {
        yield return new WaitForSeconds(0.1f);
        canDecrease = true;
    }
    private void MeleeEnergyDecrease()
    {
        canDecrease = false;
        if (MeleeEnergyStatic > 0)
        {
            MeleeEnergyStatic -= MeleeEnergyDecreaseAmount;
        }
        if(MeleeEnergyStatic <= 0 && MeleeLevelStatic > 0)
        {
            MeleeLevelStatic -= 1;
            MeleeLevelUI.text = "LV: "+ MeleeLevelStatic;
            MeleeEnergyStatic = MeleeEnergyMaxStatic;
        }
        StartCoroutine(MeleeEnergyDecreaseInterval());
    }

    private void MeleeEnergyDecreaseOfShooting()
    {
        if (MeleeEnergyStatic > 0)
        {
            MeleeEnergyStatic -= RangeEnergyPerHit;
        }
        if (MeleeEnergyStatic <= 0 && MeleeLevelStatic > 0)
        {
            MeleeLevelStatic -= 1;
            MeleeLevelUI.text = "LV: " + MeleeLevel;
            MeleeEnergyStatic = MeleeEnergyMaxStatic;
        }
    }
}
