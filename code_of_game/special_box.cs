using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class special_box : box
{
    public int get_time;
    //public NewBehaviourScript player;
    //public GameObject bullet;
    private Animator anim;
    private float x, y;
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

    //��Ծ״̬�´�������ʱ������Ч�������һ��ӣ��
    public override void jumpto()
    {
        if (get_time > 0)
        {
            anim.SetTrigger("jumped");
            Data.Instance.Cherry_add();
        }
        get_time -= 1;
        if (get_time == 0)
        {
            anim.SetTrigger("change");
        }
    }
    //ԭ�趨���Ӵ�������ӣ��
/*    public void item_create()
    {
        Instantiate(bullet,new Vector2(x,y+0.5f),transform.localRotation);
    }*/
}
