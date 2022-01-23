using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseUnit : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector2 getDirection(Transform targetPos)
    {
        Vector2 direction = (targetPos.transform.position - this.transform.position).normalized;
        return direction;
    }
    // Update is called once per frame

}
