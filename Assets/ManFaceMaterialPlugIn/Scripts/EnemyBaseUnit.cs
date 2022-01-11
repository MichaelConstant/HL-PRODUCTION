using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseUnit : MonoBehaviour
{
    public Vector2 getDirection(Transform targetPos)
    {
        Vector2 direction = (targetPos.transform.position - this.transform.position).normalized;
        return direction;
    }
    public void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}
