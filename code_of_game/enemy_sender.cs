using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sender : MonoBehaviour
{
    private bool is_touch = false;
    public GameObject bullet;
/*    public int CD;*/
    private int count;
/*    private bool send_able = true;*/
/*    private int CD_T = 5;*/
    public float CD_R = 5f;
    private System.DateTime timeLastSend = System.DateTime.MinValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*        if (is_touch == false)
                {
                    if(count == CD)
                    {
                        send();
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }
                }*/
        /*        Invoke("send", CD_T);*/
        System.DateTime timeNow = System.DateTime.Now;
        if (GetSubSeconds(timeNow, timeLastSend) > CD_R && is_touch == false)
        {
            timeLastSend = timeNow;
            // do something...
            send();
        }

    }

    void send()
    {
        Instantiate(bullet, new Vector2(transform.position.x - 2, transform.position.y), transform.localRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            is_touch= true;
        }
    }

    float GetSubSeconds(System.DateTime startTime, System.DateTime endTime)
    {
        System.TimeSpan startSpan = new System.TimeSpan(startTime.Ticks);
        System.TimeSpan endSpan = new System.TimeSpan(endTime.Ticks);
        System.TimeSpan subSpan = endSpan.Subtract(startSpan).Duration();
        return (float)subSpan.TotalSeconds;
    }

}
