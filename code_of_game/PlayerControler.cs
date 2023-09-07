using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private int count = 0;
    private int max_cd = 50;
    private bool ishurt = false;//默认为负
    public Collider2D coll;
    public float speed;
    public float jumpforce;
    private float fan_force = 800;
    public LayerMask ground;
    //public int Cherry = 0;
    /*    public Text CherryNum;
        public Data data;*/
    public GameObject bullet;
    public Transform start_point;
    bool jump_state = false;
    bool shoot_state = false;
/*    bool shoot_able = false;*/
    bool cur_state = true;
    private bool hurted = false;
    public AudioSource jumpedAudio;
    public AudioSource shootAudio;
    public AudioSource BGMAudio;
    public float hurt_time = 2f;
    private float Timer;
    private float TT = 0;
/*    int no_hurt_time = 0;
    int time = 3600;*/
    float x, y;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.DetachChildren();
        x = start_point.position.x;
        y = start_point.position.y;
        Destroy(start_point.gameObject);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ishurt)
        {
            Movement();
        }
        //检测跳跃按钮是否触发
        if (Input.GetButtonDown("Jump") && !anim.GetBool("jumping") && !anim.GetBool("falling"))
        {
            jump_state = true;
        }
        bullet_cd();
        if (shoot_state == false && Data.Instance.shoot_able == true)
        {
            bullet_create();
        }
        hurted_charge();
        SwitchAnim();
        Update();
        drop_down();
        //BGM_Check();
        //根据跳跃按钮触发情况实现跳跃

    }

    //角色跳跃与角色射击
    void Update()
    {
    
        //检测跳跃按钮是否触发
        if (Input.GetButton("Jump") && !anim.GetBool("jumping") && !anim.GetBool("falling") /*&& coll.IsTouchingLayers(ground)*/)
        {
            jump_state = true;
        }
        if (jump_state)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpedAudio.Play();
            anim.SetBool("jumping", true);
            jump_state = false;
        }
        bullet_cd();
        if (shoot_state == false && Data.Instance.shoot_able == true)
        {
            bullet_create();
        }
        hurted_charge();
        BGM_Check();

    }
    //创建弹体用于进行射击
    void bullet_create()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {

            if (transform.localScale.x > 0)
            {
                Data.Instance.shoot_speed_change(1);
                Instantiate(bullet, new Vector2(transform.position.x + 2, transform.position.y), transform.localRotation);
            }
            else
            {
                Data.Instance.shoot_speed_change(-1);
                Instantiate(bullet, new Vector2(transform.position.x - 2, transform.position.y), transform.localRotation);
            }
                
            Data.Instance.Gun_shooting();
            shootAudio.Play();
            shoot_state = true;
        }
    }
    //设置子弹射击的冷却时间
    void bullet_cd()
    {
        if (shoot_state == false)
        {
            count = 0;
        }
        else
        {
            count++;
            if (count > max_cd)
            {
                shoot_state= false;
            }

        }
    }
    //角色移动
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirecton = Input.GetAxisRaw("Horizontal");

        //角色横向移动
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed  * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirecton));
        }

        //修改角色朝向
        if (facedirecton != 0)
        {
            transform.localScale = new Vector3(facedirecton, 1, 1);
        }

    }

    void drop_down()
    {
        if(transform.position.y < -40) 
        {
            transform.position = new Vector3(x,y,transform.position.z);
            Data.Instance.live_del();
        }
    }

    //角色动画切换
    void SwitchAnim()
    {
        //anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        //切换跳跃动作
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        //切换受伤动作
        else if (ishurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            
            if (Mathf.Abs(rb.velocity.x) < 0.1 /*&& Timer >= hurt_time / 2*/)
            {
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true);
                ishurt = false;
            }
        }
        //切换回落地状态
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            //anim.SetBool("idle", true);
        }

    }

    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收藏物：樱桃
        if (collision.tag == "collection")
        {
            //Destroy(collision.gameObject);
            collision.GetComponent<Animator>().Play("isGot");

        }

        //陷阱：尖刺
        if (collision.tag == "spike")
        {
/*            if (no_hurt_time == 0)
            {
                ishurt = true;
                Data.Instance.live_del();
            }
*//*            ishurt = true;
            Data.Instance.live_del();*//*
            else if(no_hurt_time == time)
            {
                no_hurt_time = 0;
            }
            else
            {
                no_hurt_time++;
            }*/
            if (hurted == false)
            {
                ishurt = true;
                hurted = true;
                Data.Instance.live_del();

            }
            else
            {
                
            }
        }

        //远程攻击能力
        if (collision.tag == "Gun")
        {
            Destroy(collision.gameObject);
/*            shoot_able = true;*/
            Data.Instance.Get_Gun();
        }

        //存档点（告示牌）
        if (collision.tag == "sign")
        {
            Sign sign = collision.gameObject.GetComponent<Sign>();
            if (sign.sign_state == true)
            {
                x = collision.transform.position.x;
                y = collision.transform.position.y + 1;

                sign.coming();
            }

        }

        //关卡终点
        if (collision.tag == "finish")
        {
            /*            Data.Instance.ExitGame();*/
            Data.Instance.Win();
        }

        if (collision.tag == "DeadLine")
        {
            transform.position = new Vector3(x, y, transform.position.z);
            Data.Instance.live_del();
        }

        if (collision.tag == "AddShoot")
        {
            Destroy(collision.gameObject);
            Data.Instance.Add_ShootTime();
        }

        if (collision.tag == "AddLive")
        {
            Destroy(collision.gameObject);
            Data.Instance.Live_add();
        }
    }

    //特殊砖块
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //砖块：风扇（走上去后触发高跳）
        if (collision.gameObject.CompareTag("Fan"))
        {
            rb.velocity = new Vector2(rb.velocity.x, fan_force * Time.fixedDeltaTime);
        }
        //传送砖块：触碰后传送到指定位置
        if (collision.gameObject.CompareTag("teleport"))
        {
            Teleport_platform tel = collision.gameObject.GetComponent<Teleport_platform>();
            transform.position = new Vector3(tel.x, tel.y,transform.position.z);
        }
        //遭遇敌人
        if (collision.gameObject.CompareTag("enemie"))
        {
            enemy enemy_ = collision.gameObject.GetComponent<enemy>();
            //踩落攻击，销毁敌人
            if (anim.GetBool("falling"))
            {
                enemy_.JumpOn();

                jump_state = true;
            }
            //碰撞受伤
            //左方
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                if (hurted == false)
                {
                    rb.velocity = new Vector2(-4, rb.velocity.y);
                    ishurt = true;
                    Data.Instance.live_del();
                    hurted = true;
                }

            }
            //右方
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                if (hurted == false)
                {
                    rb.velocity = new Vector2(4, rb.velocity.y);
                    ishurt = true;
                    Data.Instance.live_del();
                    hurted = true;
                }
            }
        }
        //跳跃时碰到部分砖块
        if (collision.gameObject.CompareTag("box"))
        {
            box bb = collision.gameObject.GetComponent<box>();
            if (anim.GetBool("jumping"))
            {
                bb.jumpto();
            }
        }

    }

    void hurted_charge()
    {
        if (hurted == true)
        {
            Timer += Time.deltaTime;
            if (Timer >= hurt_time)
            {
                hurted = false;
                Timer = 0;
            }
        }
    }

    void hurted_change()
    {
        hurted= false;
    }

/*    public void Cherry_add()
    {
        Cherry += 1;
        CherryNum.text = Cherry.ToString();
    }*/

    void Restart()
    {
        GetComponent<AudioSource>().enabled = false;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    void BGM_Check()
    {
        if (Data.Instance.playState == false && cur_state == true)
        {
            cur_state= false;
            BGMAudio.Pause();
        }
        else if (Data.Instance.playState == true && cur_state== false)
        {
            cur_state = true;
            BGMAudio.Play();
        }

    }
}
