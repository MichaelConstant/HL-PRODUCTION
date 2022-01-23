using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }
}