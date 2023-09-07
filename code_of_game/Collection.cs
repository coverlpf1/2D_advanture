using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public bool first_come = true;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (first_come)
            {
                Data.Instance.Add_ShootTime();
                first_come = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (first_come)
            {
/*                Data.Instance.Add_ShootTime();*/
                first_come = false;
                Destroy(gameObject);
            }
        }
    }
}
