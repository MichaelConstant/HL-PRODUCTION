using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControlForStudio : CharacterControl
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        xInput = (int)Input.GetAxisRaw("Horizontal");
        yInput = (int)Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Move(xInput, yInput);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(2);
        }
    }
}