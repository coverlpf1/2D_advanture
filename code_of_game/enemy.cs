using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource deathAudio;
/*    public bool Die = false;*/
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public virtual void JumpOn()
    {
        Anim.SetTrigger("death");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        GetComponent<Collider2D>().isTrigger = true;
        deathAudio.Play();
    }
}
