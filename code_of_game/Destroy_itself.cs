using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_itself : MonoBehaviour
{
    public int live_Time = 3;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Invoke("Do", live_Time);
    }

    private void Do()
    {
        Destroy(gameObject);
    }
}
