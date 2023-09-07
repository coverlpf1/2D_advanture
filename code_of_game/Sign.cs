using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    private Animator anim;
    public GameObject enterDialog;
    private bool first_coming = true;
    //private Text Dialog;
    public string text = "这是一个存档点";
    public bool sign_state = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //enterDialog = GameObject.FindWithTag("Dialog");
        //Dialog = GameObject.FindWithTag("Dialog_text").GetComponent<Text>();
    }

    // Update is called once per frame


    //玩家触碰时
    public void coming()
    {
            if (first_coming == true)
            {
                anim.SetTrigger("coming");
                first_coming = false;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Dialog.text = text;
            Data.Instance.dialog_text_change(text);
            enterDialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enterDialog.SetActive(false);
        }
    }
}
