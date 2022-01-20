using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : Bullet
{
    private SpriteRenderer sr;

    public Sprite Bullet_0;
    public Sprite Bullet_1;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int BulletType = Random.Range(0, 2);
        if (BulletType == 0)
        {
            sr.sprite = Bullet_0;
        }
        else
        {
            sr.sprite = Bullet_1;
        }
        Destroy(gameObject, BulletLifeSpan);
    }
    // Update is called once per frame
    void Update()
    {
        float fadePerSecond = (sr.color.a / BulletLifeSpan);
        Color tempColor = sr.color;
        tempColor.a -= fadePerSecond * Time.deltaTime;
        sr.color = tempColor;
    }
}