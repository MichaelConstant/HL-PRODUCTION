using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public SpriteRenderer sr;

    public enum doorDirection { up, down, left, right };
    public doorDirection DoorDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Direction", (int)DoorDirection);
    }
}