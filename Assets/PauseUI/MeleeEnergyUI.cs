using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnergyUI : MonoBehaviour
{
    public Image energySign;
    private int meleeLevel;
    private float meleeEnergy;
    private float meleeEnergyMax;
    private float percent;

    // Start is called before the first frame update
    void Start()
    {
        energySign.color = new Color32(255, 249, 117, 0);
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
                energySign.color = new Color32(255, 249, 117, (byte)(255 * meleeEnergy / meleeEnergyMax));
                if (meleeEnergy == meleeEnergyMax)
                {
                    energySign.color = new Color32(255, 249, 117, 0);
                }
                break;
            case 1:
                energySign.color = new Color32(255, (byte)(249 - (249 - 45) * percent), (byte)(117 - (117 - 49) * percent), 240);
                break;
            case 2:
                energySign.color = new Color32(255, (byte)(45 + (140 - 45) * percent), (byte)(49 + (255 - 49) * percent), 240);
                break;
            case 3:
                energySign.color = new Color32(45, 140, 255, 240);
                break;

        }
    }
}

