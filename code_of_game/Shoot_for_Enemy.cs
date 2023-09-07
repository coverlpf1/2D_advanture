using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_for_Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isTouching = false;
    public float speed;
    public GameObject bullet;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
/*        rb.velocity = new Vector2(speed, rb.velocity.y);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    public void OnTriggerEnter2D(Collider2D collision)
    {
*//*        if (coll.IsTouchingLayers(ground))*//*
        {
            if (isTouching == false)
            {
                Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), transform.localRotation);
                isTouching = true;
                Destroy(gameObject);
            }
        }

    }*/

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (coll.IsTouchingLayers(ground) || collision.gameObject.tag == "Player")
        {
            if (isTouching == false)
            {
                Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), transform.localRotation);
                isTouching = true;
                Destroy(gameObject);
            }
        }

    }
}
