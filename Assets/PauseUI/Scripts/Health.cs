using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int health;
    public int numOfHearts;//const int heart number,only change if the design changes
    private int maxHealth;


    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    bool hasHalfHeart;
    private void Start()
    {

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void Update()
    {
        health = (int)GetComponent<PlayerControl>().currentHP;
        maxHealth = (int)GetComponent<PlayerControl>().maxHP;
        HealthUpdate(health);

    }

    void HealthUpdate(float health)//PlayerHealthBarUISystem
    {
        hasHalfHeart = false;
        for (int i = 0; i < hearts.Length; i++)
        {
            //In case the Design changes afterwards
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

            if (health >= (i + 1) * 2)
            {
                hearts[i].enabled = true;
                hearts[i].sprite = fullHeart;
            }
            else if (health % 2 != 0 && (!hasHalfHeart))
            {
                hearts[i].enabled = true;
                hearts[i].sprite = halfHeart;
                hasHalfHeart = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    //if the image array is bigger than the set int "numOfHearts",then the heart images  should be hidden

}
