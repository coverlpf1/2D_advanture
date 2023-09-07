using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_rocket : enemy
{

    private Rigidbody2D rb;
    public float speed = -1;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        if (speed > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Anim.SetTrigger("death");
    }

    public void speed_change(float speed_x)
    {
        speed = speed_x;
    }
}
