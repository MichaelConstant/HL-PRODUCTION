using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transport : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadScene(3);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                SceneManager.LoadScene(4);
            }
        }
    }
}