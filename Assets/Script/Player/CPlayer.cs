using UnityEngine;
using Public;
using System.Collections;

public class CPlayer : CSigleton<CPlayer>
{
    public float m_speed_high = 4f;
    public float m_speed_low = 2f;

    [Header("属性")]
    [SerializeField] private int _HP;   
    public int HP
    {
        get => _HP;
        set
        {
            if (value == _HP) return;
            _HP = value;
            CEventSystem.Instance.ActivateEvent(EEventType.PlayerChange, value);
        }
    }

    [SerializeField] private int _NumOfBomb;  
    public int NumOfBomb
    {
        get => _NumOfBomb;
        set
        {
            if (value == _NumOfBomb) return;
            _NumOfBomb = value;
            CEventSystem.Instance.ActivateEvent(EEventType.SpellCardChange, value);
        }
    }

    [SerializeField] private int _Power;  //火力
    public int Power
    {
        get => _Power;
        set
        {
            if (value == _HP) return;
            _Power = value;
            CEventSystem.Instance.ActivateEvent(EEventType.PowerChange, value);
        }
    }

    [Header("状态")]
    [SerializeField] private bool _InSlowMode;    //低速模式
    public bool InSlowMode
    {
        get => _InSlowMode;
        set
        {
            if (value == _InSlowMode) return;
            _InSlowMode = value;
            CEventSystem.Instance.ActivateEvent(EEventType.SlowModeOn, value);
        }
    }

    private float t_shoot = 0.2f;
    private bool b_canShoot = true;
    private bool b_wantShoot;
    private float angle_deviation_high = 10f;   //高速移动下，射击时角度偏差范围
    private float angle_deviation_low = 5f;     //低速移动下，射击时角度偏差范围
    private float angle_deviation_idle = 1f;    //静止时，射击时角度偏差范围

    private bool b_wantThrowBomb;

    internal bool b_isMoving;
    internal Vector2 drct_move;
    internal float angle_move;
    internal float m_speed_rotate;  //每秒转过的角度
    internal Vector3 m_pos;

    internal Vector2 drct_shoot;
    internal float angle_shoot;

    private Rigidbody2D m_Rigidbody;

    protected override void Awake()
    {
        base.Awake();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        HP = 30;
        Power = 100;
        NumOfBomb = 3;
    }

    private void Update()
    {
        InputCheck();
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        Move();
        ThrowBomb();
        Shoot();
    }

    private void InputCheck()
    {
        drct_move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        InSlowMode = Input.GetKey(KeyCode.LeftShift);
        b_wantShoot = Input.GetKey(KeyCode.Z);
        if (Input.GetKeyDown(KeyCode.X)) b_wantThrowBomb = true;
    }

    private void PhysicsCheck()
    {
        m_pos = transform.position;
        b_isMoving = m_Rigidbody.velocity.magnitude > 0.1f;
    }

    private void Move()
    {
        //待修改
        m_Rigidbody.velocity = drct_move * (InSlowMode ? m_speed_low : m_speed_high);
    }

    private void Shoot()
    {
        void Shoot_(int bulletIndex, Vector3 offset, int angle)
        {
            CBulletController.Instance.Shoot(bulletIndex, m_pos + offset, angle);
        }

        if (!b_canShoot) return;
        StartCoroutine(ShootCoolDown());
    }
    private IEnumerator ShootCoolDown()
    {
        b_canShoot = false;
        yield return CTool.Wait(t_shoot);
        b_canShoot = true;
    }

    private void ThrowBomb()
    {
        if (b_wantThrowBomb && NumOfBomb > 0)
        {
            NumOfBomb--;

            b_wantThrowBomb = false;
        }
    }
}
