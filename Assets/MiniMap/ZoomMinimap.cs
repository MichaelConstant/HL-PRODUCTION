using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomMinimap : MonoBehaviour
{
    bool zoomed = false;
     GameObject miniMapCamera;
    private void Start()
    {
        miniMapCamera = GameObject.FindWithTag("MiniMapCamera");
        miniMapCamera.GetComponent<Camera>().orthographicSize = 15;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&&(!zoomed))
        {
            this.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(880, 445);
            //this.transform.parent.GetComponent<RectTransform>().transform.position = new Vector3(40, 0, 0);
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(800,800);
            miniMapCamera.GetComponent<Camera>().orthographicSize = 35;
            zoomed = true;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && (zoomed))
        {
            this.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(185, 125);
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            miniMapCamera.GetComponent<Camera>().orthographicSize = 15;
            zoomed = false;
        }
    }
}
