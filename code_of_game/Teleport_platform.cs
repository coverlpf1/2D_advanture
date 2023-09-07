using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_platform : MonoBehaviour
{
    public Transform tele_point;
    public float x, y;
    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
        x = tele_point.position.x;
        y = tele_point.position.y;
        Destroy(tele_point.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
