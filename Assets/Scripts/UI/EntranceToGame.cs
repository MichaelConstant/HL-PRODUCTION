using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceToGame : MonoBehaviour
{
    public GameObject m_PlayerUI;
    public GameObject phoneUI;
    public GameObject cinematic;
    private void Start()
    {
        m_PlayerUI.SetActive(false);
        if(FirstAnimation.isDead == true)
        {
            phoneUI.SetActive(false);
            cinematic.SetActive(true);
            FirstAnimation.isDead = false;
        }

    }
    private void FixedUpdate()
    {
        if (cinematic.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            cinematic.SetActive(false);
            phoneUI.SetActive(true);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<PlayerControl>() != null)&&Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene(2);
            m_PlayerUI.SetActive(true);
        }
    }
}