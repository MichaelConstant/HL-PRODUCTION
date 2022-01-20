using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KouKou : CharacterControl
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AttackVector = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;

        rb.velocity = AttackVector * movementSpeed;

        if(rb.velocity.x>0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        AnimatorStateInfo Info = anim.GetCurrentAnimatorStateInfo(0);
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            if (currentHP <= 0)
            {
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                Destroy(gameObject);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk2"))
            {
                anim.Play("Walk1");
                CommonShoot();
            }
            else
            {
                anim.Play("Walk2");
                CommonShoot();
            }
        }
        if (currentHP <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.Play("Dead");
        }
    }
}
