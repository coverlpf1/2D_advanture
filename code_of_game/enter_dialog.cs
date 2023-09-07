using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_dialog : MonoBehaviour
{
    public GameObject enterDialog;
    public string text = "°´E¼ü½øÈë";
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
