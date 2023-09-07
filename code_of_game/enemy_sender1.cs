using UnityEngine;
using MY_FSM;
using System;

[Serializable]
public class SenderBlacboard : Blackboard
{
    public float idleTime;
    public float moveSpeed;
    public Transform transform;
    public float maxDistance;
    public GameObject player;
    public Animator anim;
    public GameObject self;
    public bool hurtState = false;
    public GameObject bullet;
    public int CD = 3;
    public int Cur_CD = 3;
    public int JumpForce = 600;
    public int Speed = 200;
    public bool JumpState = false;
}

public class IdleState : IState
{
    private float idleTimer;
    private FSM fsm;
    private SenderBlacboard blackboard;
    public IdleState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as SenderBlacboard;
    }
    public void OnEnter()
    {
        idleTimer = 0;
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        if(Mathf.Abs(blackboard.transform.position.x - blackboard.player.transform.position.x) < blackboard.maxDistance)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > blackboard.idleTime)
            {
                if (blackboard.Cur_CD == blackboard.CD)
                {
                    this.fsm.SwitchState(StateType.Far_Attack);
                    blackboard.Cur_CD = 0;
                }
                else
                {
                    this.fsm.SwitchState(StateType.Attack);
                    blackboard.Cur_CD += 1;
                }

            }
        }
    }
}

public class AttackState : IState
{
    private FSM fsm;
    private SenderBlacboard blackboard;
    private Vector2 targetPos;
    private float Timer = 0;

    public AttackState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as SenderBlacboard;
    }
    public void OnEnter()
    {
        float x = blackboard.player.transform.position.x;
        float y = blackboard.transform.position.y;
        targetPos = new Vector2(x, y);
        blackboard.anim.SetBool("run", true);
        if (x < blackboard.transform.position.x)
        {
            blackboard.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            blackboard.transform.localScale = new Vector3(1, 1, 1);
        }
        Timer = 0;
    }

    public void OnExit()
    {
        blackboard.anim.SetBool("run", false);
    }

    public void OnUpdate()
    {
        if (Vector2.Distance(blackboard.transform.position, targetPos) < 0.1f)
        {
            fsm.SwitchState(StateType.Idle);
        }
        else
        {
            blackboard.transform.position = Vector2.MoveTowards(blackboard.transform.position,targetPos,blackboard.moveSpeed* Time.deltaTime);
            Timer += Time.deltaTime;
            if (Timer >= blackboard.idleTime * 2)
            {
                this.fsm.SwitchState(StateType.Find_Enemy);
            }
        }
    }
}

public class Jump_AttackState : IState
{
    private FSM fsm;
    private SenderBlacboard blackboard;
    private Vector2 targetPos;
/*    private float Timer = 0;*/
    private int Speed = 0;
    private bool state = false;

    public Jump_AttackState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as SenderBlacboard;
    }
    public void OnEnter()
    {
        state = false;
        float x = blackboard.player.transform.position.x;
        float y = blackboard.transform.position.y;
        targetPos = new Vector2(x, y);
        blackboard.anim.SetBool("run", true);
        if (x < blackboard.transform.position.x)
        {
            blackboard.transform.localScale = new Vector3(-1, 1, 1);
            Speed = -1 * blackboard.Speed;
        }
        else
        {
            blackboard.transform.localScale = new Vector3(1, 1, 1);
            Speed =  blackboard.Speed;
        }
/*        Timer = 0;*/
    }

    public void OnExit()
    {
        blackboard.anim.SetBool("run", false);
    }

    public void OnUpdate()
    {
        if(!state)
        {
            blackboard.self.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * Time.fixedDeltaTime, blackboard.JumpForce * Time.fixedDeltaTime);
            state = true;
        }
        if (state && blackboard.JumpState == false)
        {
            blackboard.JumpState = true;
            this.fsm.SwitchState(StateType.Attack);
        }
        else
        {
            blackboard.JumpState = false;
            this.fsm.SwitchState(StateType.Idle);
        }

/*            blackboard.transform.position = Vector2.MoveTowards(blackboard.transform.position, targetPos, blackboard.moveSpeed * Time.deltaTime);*/
/*            Timer += Time.deltaTime;
            if (Timer >= blackboard.idleTime)
            {
            this.fsm.SwitchState(StateType.Idle);
            }*/
    }
}

public class Far_AttackState : IState
{
    private FSM fsm;
    private SenderBlacboard blackboard;
    private Vector2 targetPos;


    public Far_AttackState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as SenderBlacboard;
    }
    public void OnEnter()
    {
        blackboard.anim.SetBool("attack", true);
    }

    public void OnExit()
    {
        blackboard.anim.SetBool("attack", false);
    }

    public void OnUpdate()
    {
/*        if (Vector2.Distance(blackboard.transform.position, targetPos) < 0.1f)
        {
            fsm.SwitchState(StateType.Idle);
        }
        else
        {
            blackboard.transform.position = Vector2.MoveTowards(blackboard.transform.position, targetPos, blackboard.moveSpeed * Time.deltaTime);
        }*/
    }
}

public class HurtState : IState
{
    private FSM fsm;
    private SenderBlacboard blackboard;
    private float hurtTimer;

    public HurtState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as SenderBlacboard;
    }
    public void OnEnter()
    {
        blackboard.anim.SetBool("hurt", true);
        hurtTimer = 0;
        blackboard.hurtState = true;
        blackboard.self.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        blackboard.self.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        blackboard.self.GetComponent<Collider2D>().isTrigger = true;
    }

    public void OnExit()
    {
        blackboard.anim.SetBool("hurt", false);
        blackboard.self.GetComponent<Collider2D>().isTrigger = false;
        blackboard.self.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        blackboard.self.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        blackboard.hurtState = false;
    }

    public void OnUpdate()
    {
        hurtTimer += Time.deltaTime;
        if (hurtTimer > blackboard.idleTime / 2)
        {
            this.fsm.SwitchState(StateType.Idle);
        }
    }
}

public class DeathState : IState
{
    private FSM fsm;
    private SenderBlacboard blackboard;


    public DeathState(FSM fsm)
    {
        this.fsm = fsm;
        this.blackboard = fsm.blackboard as SenderBlacboard;
    }
    public void OnEnter()
    {
        blackboard.self.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        blackboard.self.GetComponent<Collider2D>().isTrigger = true;
        blackboard.anim.SetTrigger("death");
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
       
    }
}

public class enemy_sender1 : enemy
{
    //默认具有三次生命值
    public int lives = 3;
    // Start is called before the first frame update
    private FSM fsm;
    public SenderBlacboard blackboard;
    public GameObject bullet;
    private int cur_time = 0;
    public int max_time = 3;
    public bool is_key = true;
    public GameObject door;
    protected override void Start()
    {
        fsm = new FSM(blackboard);
        fsm.AddState(StateType.Idle, new IdleState(fsm));
        fsm.AddState(StateType.Attack, new AttackState(fsm));
        fsm.AddState(StateType.Find_Enemy, new Jump_AttackState(fsm));
        fsm.AddState(StateType.Far_Attack, new Far_AttackState(fsm));
        fsm.AddState(StateType.hurt, new HurtState(fsm));
        fsm.AddState(StateType.Die, new DeathState(fsm));
        fsm.SwitchState(StateType.Idle);
        blackboard.player =GameObject.FindGameObjectWithTag("Player");
        blackboard.anim = GetComponent<Animator>();
        blackboard.self = gameObject;
        blackboard.bullet = bullet;
        
    }

    // Update is called once per frame
    void Update()
    {
        fsm.OnCheck();
        fsm.OnUpdate();
    }

    private void FixedUpdate()
    {
        fsm.OnFixUpdate();
    }

    public override void JumpOn()
    {
        if (blackboard.hurtState == false)
        {
            if (lives > 1)
            {
                fsm.SwitchState(StateType.hurt);
                lives -= 1;
            }
            else
            {
                fsm.SwitchState(StateType.Die);
            }
        }

    }

    public void DO()
    {
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), transform.localRotation);
        if (is_key)
        {
            Destroy(door);
        }
        Destroy(gameObject);
    }

    public void Attack()
    {
        Instantiate(bullet, new Vector2(blackboard.player.transform.position.x , blackboard.player.transform.position.y + 2), transform.localRotation);
/*        cur_time += 1;*/
        if (cur_time++ == max_time)
        {
            cur_time = 0;
            fsm.SwitchState(StateType.Idle);
        }
    }
}
