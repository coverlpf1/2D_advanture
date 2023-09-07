using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_box : switch_box
{


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
/*        coll.isTrigger = false;
        rend.sortingOrder = 1;*/

        coll.isTrigger = true;
        rend.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    public void Change()
    {
        coll.isTrigger = true;
        rend.sortingOrder = -1;
    }*/
}
