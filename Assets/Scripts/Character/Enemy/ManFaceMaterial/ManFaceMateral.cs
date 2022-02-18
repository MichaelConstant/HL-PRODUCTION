using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManFaceMateral : CharacterControl
{
    int state;
    int lastState = 0;
    //0:Idle, 1:Jump, 2:Pour

    GameObject BossHP;

    void Start()
    {
        AttackVector = (GameObject.FindWithTag("Player").transform.position - transform.position);
        BossHP = GameObject.FindWithTag("GameUI").transform.GetChild(0).gameObject;
        BossHP.SetActive(true);
        Idle();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        BossHP.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)currentHP / maxHP;

        AttackVector = (GameObject.FindWithTag("Player").transform.position - transform.position);

        if (currentHP <= 0)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector3(0, 0, 0);
            BossHP.SetActive(false);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            anim.Play("Dead");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            if (currentHP <= 0)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(gameObject);
                BossHP.SetActive(false);
            }

            int rand = Random.Range(0, 100);

            if (rand >= 0 && rand < 80)
            {
                state = 1;
            }
            else if (rand >= 80 && rand < 90)
            {
                if (lastState == 0)
                {
                    state = 1;
                }
                else
                {
                    state = 0;
                }
            }
            else if (rand >= 90 && rand < 100)
            {
                if (lastState == 2)
                {
                    state = 1;
                }
                else
                {
                    state = 2;
                }
            }

            switch (state)
            {
                case 0:
                    Idle();
                    lastState = 0;
                    break;
                case 1:
                    StartCoroutine(Jump());
                    lastState = 1;
                    break;
                case 2:
                    LookDownPour();
                    lastState = 2;
                    break;
            }
        }
        
    }
    private IEnumerator Jump()
    {
        rb.AddForce((Vector2)transform.up * 100f + AttackVector.normalized * movementSpeed_Final);
        rb.gravityScale = 0.4f;
        anim.Play("Jump");
        yield return new WaitForSeconds(0.98f);
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0, 0, 0);
    }
    private void Idle()
    {
        anim.Play("Idle");
    }
    private void LookDownPour()
    {
        anim.Play("LookDownPour");
        CommonShoot();
    }
}