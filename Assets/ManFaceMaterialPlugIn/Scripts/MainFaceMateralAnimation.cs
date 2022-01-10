using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFaceMateralAnimation : EnemyBaseUnit
{
    protected bool canJump = false;
    // Start is called before the first frame update
    protected void JumpInParent()
    {
        gameObject.GetComponentInParent<ManFaceMaterial>().Jump();
    }
    protected void StopJumpInParent()
    {
        gameObject.GetComponentInParent<ManFaceMaterial>().StopJump();
    }
    protected void LookDownPourParent()
    {
        gameObject.GetComponentInParent<ManFaceMaterial>().LookDownPour ();
    }
    protected void CountIdldeTimeParent()
    {
        gameObject.GetComponentInParent<ManFaceMaterial>().CountIdleTime();
    }
}
