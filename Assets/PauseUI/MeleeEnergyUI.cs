using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnergyUI : MonoBehaviour
{
    public Image energySign;
    private int meleeLevel;
    public Sprite green;
    public Sprite blue;
    public Sprite red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(meleeLevel);
        meleeLevel = this.GetComponent<PlayerControl>().MeleeLevel;
        switch (meleeLevel) {
            case 0:
                energySign.sprite = green;break;
            case 1:
                energySign.sprite = blue;break;
            case 2:
                energySign.sprite = red;break;

        }
    }
}
