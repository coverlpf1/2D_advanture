using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_box : box
{
    public GameObject item;
    private Animator anim;
    private float x, y;
    private bool jump_able = true;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        //player = find<NewBehaviourScript>();
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void jumpto()
    {
        if (jump_able == true)
        {
            anim.SetTrigger("jumped");
            /*       Invoke("item_create", 1f);*/
            item_create();
            anim.SetTrigger("change");
            jump_able= false;
        }
    }
    //创建当前道具
    public void item_create()
    {
        Instantiate(item, new Vector2(x, y + 1f), transform.localRotation);
    }
}
