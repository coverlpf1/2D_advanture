using UnityEngine;

public class fall_platform : MonoBehaviour
{
    private Material material;
    public float direction_max;
    public float broadwise_max;
    public float direction_speed;
    public float broadwise_speed;
    private float x;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        x = transform.localPosition.x;
        y = transform.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.localPosition.x - x) >= broadwise_max)
        {
            broadwise_speed *= -1;
        }

        if (transform.localPosition.y - y >= direction_max)
        {
            direction_speed *= -1;
        }
        else if (transform.localPosition.y - y <= direction_max * (-1))
        {
            direction_speed *= -1;
        }
        transform.localPosition = new Vector3(transform.localPosition.x + broadwise_speed * Time.fixedDeltaTime, transform.localPosition.y + direction_speed * Time.fixedDeltaTime, transform.localPosition.z);
        
    }


}
