using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    Animator anim;
    int BeamDamage;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (GetComponentInParent<Cock>().GetComponent<SpriteRenderer>().flipX == true)
        {
            anim.Play("BeamPlay_Right");
        }
        else
        {
            anim.Play("BeamPlay_Left");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            collision.gameObject.GetComponent<PlayerControl>().currentHP -= BeamDamage;
        }
    }
}
