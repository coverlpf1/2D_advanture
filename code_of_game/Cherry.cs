using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    bool first_come = true;
    public void Death()
    {
        if (first_come)
        {
            first_come = false;
            Data.Instance.Cherry_add();
        }
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (first_come)
            {
                Data.Instance.Cherry_add();
                first_come = false;
            }
        }

       

    }
}
