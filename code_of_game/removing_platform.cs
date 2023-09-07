using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removing_platform : MonoBehaviour
{
    public float sleep_time;
    private Collider2D coll;
    private Renderer rend;
    public float Timer = 0;
    public bool state = false;
    private 
    // Start is called before the first frame update
    void Start()
    {
        coll= GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == false)
        {

        }
        else if(state == true)
        {
            Timer += Time.fixedDeltaTime;
            if (Timer >= sleep_time && Timer < sleep_time * 3)
            {
                if(coll.isTrigger == false)
                {
                    run();
                }
            }
            else if (Timer >= sleep_time * 2)
            {
                Rerun();
                Timer = 0;
                state = false;
            }
        }


    }
    //玩家走上去一段时间后开始消失，消失一段时间后复原
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
/*            Invoke("run", sleep_time);
            Invoke("Rerun", sleep_time * 3);*/
            state = true;
        }
    }

    private void run()
    {
        //Destroy(gameObject);
        coll.isTrigger= true;
        rend.sortingLayerName = "Default";
    }

    private void Rerun()
    {
        coll.isTrigger= false;
        rend.sortingLayerName = "ground";
    }
}
