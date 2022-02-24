using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType { Unbreakable, BreakableOnRage, Breakable };
    public ObstacleType obstacleType;
    public int ObstacleHP;

    GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<FiredBullet>() != null)
        {
            switch(obstacleType)
            {
                case ObstacleType.Unbreakable:
                    Destroy(collision.gameObject);
                    break;
                case ObstacleType.BreakableOnRage:
                    Destroy(collision.gameObject);
                    if (Player.GetComponent<PlayerControl>().onRage)
                    {
                        ObstacleHP--;
                        gameObject.transform.localScale *= 0.9f;
                        if(ObstacleHP<=0)
                        {
                            Destroy(gameObject);
                        }
                    }
                    break;
                case ObstacleType.Breakable:
                    Destroy(collision.gameObject);
                    ObstacleHP--;
                    gameObject.transform.localScale *= 0.9f;
                    if (ObstacleHP <= 0)
                    {
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}
