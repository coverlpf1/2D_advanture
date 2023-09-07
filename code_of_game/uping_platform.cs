using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class uping_platform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tran;
    private float Topy;
    private float Buttony;
    public Transform Top;
    public Transform Button;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        tran= GetComponent<Transform>();
        transform.DetachChildren();
        Topy = Top.transform.position.y;
        Buttony = Button.transform.position.y;
        Destroy(Top.gameObject);
        Destroy(Button.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(transform.position.y < Topy)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
        else
        {
            tran.position = new Vector3(tran.position.x,Buttony,tran.position.z);
        }
    }
}
