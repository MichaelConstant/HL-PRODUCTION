using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudioDoorControl : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<PlayerControl>() != null) && Input.GetKey(KeyCode.E))
        {
            Application.Quit();
        }
    }
}
