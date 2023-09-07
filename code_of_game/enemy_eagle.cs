using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_eagle : enemy
{
    private Rigidbody2D rb;
    //private Collider2D coll;
    private float topy, buttony;
    public float Speed;
    public Transform Top_point, Button_point;
    private bool FaceTop = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //coll= rb.GetComponent<Collider2D>();
        transform.DetachChildren();
        topy = Top_point.position.y;
        buttony = Button_point.position.y;
        Destroy(Top_point.gameObject);
        Destroy(Button_point.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (FaceTop)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > topy)
            {
                FaceTop = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < buttony)
            {
                FaceTop = true;
            }


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FaceTop = !FaceTop;
    }
}
