using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceToGame : MonoBehaviour
{
    public GameObject cinematic;
    public GameObject playerUI;
    public Inventory myBag;
    public GameObject boop;
    public GameObject E;

    private Animator anim;
    private void Start()
    {
        E.SetActive(false);
        anim = boop.GetComponent<Animator>();
        myBag.itemList.Clear();
        InventoryManager.RefreshItem();
        playerUI.SetActive(false);
        if (FirstAnimation.isDead == true)
        {
            cinematic.SetActive(true);
            FirstAnimation.isDead = false;
        }

    }
    private void FixedUpdate()
    {
        if (cinematic.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            cinematic.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            anim.SetTrigger("Approach");
            E.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<PlayerControl>() != null)&&Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("LV1");
            playerUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerControl>() != null)
        {
            anim.SetTrigger("Left");
            E.SetActive(false);
        }
    }
}