using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public int needCherry = 30;
    public Transform item_position;

    public GameObject sell_item;
    public GameObject enterDialog;
    public string text = "使用30枚樱桃换取道具，此处为增加子弹上限";
    private string error = "你的樱桃数目不足";
    private float x, y;
    public bool isTouching = false;
    public bool cherryEnough = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
        x = item_position.position.x;
        y = item_position.position.y;
        Destroy(item_position.gameObject);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTouching == false)
        {
            isTouching = true;
        }
        Data.Instance.dialog_text_change(text);
        enterDialog.SetActive(true);
        if (/*Input.GetKeyDown(KeyCode.Q) && **//*//*collision.GetComponent<Animator>().GetBool("jumping") == true*/ isTouching == true && cherryEnough == true)
        {
            if (Data.Instance.Cherry < needCherry)
            {
                text = error;
/*                Data.Instance.dialog_text_change(error);*/
                cherryEnough = false;
            }
            else
            {
                Data.Instance.Cherry_del(needCherry);
                Instantiate(sell_item, new Vector2(x,y), transform.localRotation);
                cherryEnough = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enterDialog.SetActive(false);
            isTouching = false;
            cherryEnough = true;
        }
    }

/*    void CherryNotEnough()
    {


    }*/
}
