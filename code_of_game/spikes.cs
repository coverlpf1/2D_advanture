using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{
    public float speed = -3f;
    private GameObject player;
    private Rigidbody2D rb;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        x = player.transform.position.x;
        wakeup();
        destroy_it();
    }

    void wakeup()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        }
    }

    void destroy_it()
    {
        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }
}
