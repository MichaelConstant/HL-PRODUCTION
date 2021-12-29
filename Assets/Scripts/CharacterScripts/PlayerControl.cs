using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : CharacterControl
{
    public Image MeleeEnergyUI;
    public Text MeleeLevelUI;

    public int HP = 10;

    public static float RangeEnergy;
    public static float MeleeEnergy;

    public static float RangeEnergyPerHit;
    public static float MeleeEnergyPerHit;

    public float RangeEnergyDecreaseAmount;
    public float MeleeEnergyDecreaseAmount;
    private bool canDecrease=true;

    public static int RangeLevel;
    public static int MeleeLevel;

    public static int RangeEnergyMax;
    public static int MeleeEnergyMax;
    public static int MeleeLevelMax;

    private float MeleePercent;

    // Start is called before the first frame update
    void Start()
    {
        RangeEnergy = 0f;
        MeleeEnergy = 0f;

        RangeEnergyPerHit = 1f;
        MeleeEnergyPerHit = 1f;

        RangeLevel = 0;
        MeleeLevel = 0;

        RangeEnergyMax = 10;
        MeleeEnergyMax = 5;
        MeleeLevelMax = 3;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Move(xInput, yInput);

        if (Input.GetMouseButton(0) && canShoot)
        {
            Shoot();
        }

        if (Input.GetMouseButton(1) && canAttack)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        MeleePercent = (float)MeleeEnergy / MeleeEnergyMax;
        MeleeEnergyUI.fillAmount = MeleePercent;


        if (canDecrease == true)
        {
            MeleeEnergyDecrease();
            MeleeLevelUI.text = "LV: " + MeleeLevel;
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
        if (MeleeEnergy > 0)
        {
            MeleeEnergy -= MeleeEnergyDecreaseAmount;
        }
        if(MeleeEnergy <= 0 && MeleeLevel>0)
        {
            MeleeLevel -= 1;
            MeleeLevelUI.text = "LV: "+ MeleeLevel;
            MeleeEnergy = MeleeEnergyMax;
        }
        StartCoroutine(MeleeEnergyDecreaseInterval());
    }
}
