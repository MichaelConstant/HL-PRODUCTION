using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man_Face_Material : MonoBehaviour
{
    private float nextFire = 0.0F;
    // Start is called before the first frame update

       void Update()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + 1f;
            Jump();
        }
    }
    void Jump()
    {
        
    }
}
