using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_frog : enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D Coll;
    public LayerMask ground;
    private float leftx, rightx;
    public Transform leftpoint;
    public Transform rightpoint;
    public float Speed,jumpforce;
    private bool Faceleft = true;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if (Faceleft)
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
            if (Coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, jumpforce);
            }
 

        }
        else
        {
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
            if (Coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, jumpforce);
            }


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("enemie"))
        {
            if (Faceleft == true)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }

    }
    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }

        if (Coll.IsTouchingLayers(ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }

}
