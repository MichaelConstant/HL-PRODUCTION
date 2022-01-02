using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    Vector3 CamPos;
    Vector3 RoomPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CamPos = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,-2);
        RoomPos = new Vector3(this.transform.position.x, this.transform.position.y,-2);
        if (collision.GetComponent<PlayerControl>()!=null)
        {
            Camera.main.transform.position = Vector3.MoveTowards(CamPos, RoomPos, 10*Time.deltaTime);
        }
    }
}
