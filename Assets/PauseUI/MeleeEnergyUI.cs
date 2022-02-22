using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnergyUI : MonoBehaviour
{
    public Image energySign;
    private int meleeLevel;
    private int currentLevel;
    private float meleeEnergy;
    private float meleeEnergyMax;
    private float percent;
    [Header("能量条变化的三种颜色")]
    public Color32 defaultColor;
    public Color32 level1Color;
    public Color32 level2Color;
    public Color32 level3Color;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = meleeLevel;
        energySign.color = defaultColor;


    }

    // Update is called once per frame
    void Update()
    {
        meleeLevel = this.GetComponent<PlayerControl>().MeleeLevel;
        meleeEnergy = this.GetComponent<PlayerControl>().MeleeEnergy;
        meleeEnergyMax = this.GetComponent<PlayerControl>().MeleeEnergyMax;
        percent = meleeEnergy / meleeEnergyMax;

        switch (meleeLevel)
        {
            case 0:
                energySign.color = new Color32(level1Color.r, level1Color.g, level1Color.b, (byte)(255 * meleeEnergy / meleeEnergyMax));
                if ((meleeEnergy == meleeEnergyMax) || (meleeEnergy == 0))
                {
                    energySign.color = defaultColor;
                }
                break;
            case 1:
                energySign.color = new Color32((byte)(level1Color.r + (level2Color.r - level1Color.r) * percent), (byte)(level1Color.g + (level2Color.g - level1Color.g) * percent), (byte)(level1Color.b + (level2Color.b - level1Color.b) * percent), 240);
                break;
            case 2:
                energySign.color = new Color32((byte)(level2Color.r + (level3Color.r - level2Color.r) * percent), (byte)(level2Color.g + (level3Color.g - level2Color.g) * percent), (byte)(level2Color.b + (level3Color.b - level2Color.b) * percent), 240);
                break;

        }
        //Debug.Log($"Level:{meleeLevel},Energy:{meleeEnergy}");

        
        if (meleeEnergy <= 0)
        {
            meleeEnergy = 0;
            energySign.color = defaultColor;
        }
        if (meleeEnergy == meleeEnergyMax)
        {
            Shake();
        }
       else if (meleeLevel > currentLevel)
        {
            Shake();
        }
        StartCoroutine(Wait(5f));
        currentLevel = meleeLevel;
    }
    private float time;
    private float intervaltime=0.1f;
    void Shake()
    {
        time += Time.deltaTime;
        if (time >= intervaltime)
        {
            energySign.color = new Color(energySign.color.r, energySign.color.g, energySign.color.b, 0);
            time = 0;
        }
        else if(time>0.05f)
        {
            energySign.color = new Color(energySign.color.r, energySign.color.g, energySign.color.b, 255);
        }
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

}