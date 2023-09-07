using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float max_size;
    private float start_loc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Data.Instance.shoot_speed , 0);
        start_loc = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - start_loc) > max_size)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemie"))
        {
            enemy enemy_ = collision.gameObject.GetComponent<enemy>();
            enemy_.JumpOn();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
