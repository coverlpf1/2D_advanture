using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_CTL : MonoBehaviour
{
    private switch_box[] switch_boxs;
    private Animator anim;
    private bool on = true;
    // Start is called before the first frame update
    void Start()
    {
        switch_boxs = FindObjectsOfType<switch_box>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on != Data.Instance.on)
        {
            anim.SetTrigger("change");
            on = !on;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "enemie" )
        {
            act();
            Data.Instance.switch_case();
        }

    }

    void act()
    {
        for (int i = 0; i < switch_boxs.Length; i++)
        {
            switch_boxs[i].Change();
        }
    }
}
