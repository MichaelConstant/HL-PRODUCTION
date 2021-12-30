using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : CharacterControl
{
    private float nextFire = 0.0F;
    public int EnemyHP = 10;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("BOSS HP: " + EnemyHP);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time+ 4f;
            anim.SetBool("isJump", true);
            Debug.Log("sdfsdf");
             }
    }
    private void StopJump()
    {
        anim.SetBool("isJump", false);
    }
}
