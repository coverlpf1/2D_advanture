using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanPlatform : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.Play("run_fan_platform");
        }
    }
}
