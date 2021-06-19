using Public;
using System.Collections;
using UnityEngine;

public class CPlayer : CSigleton<CPlayer>
{

    [Header("属性")]
    [SerializeField] private int _HP;
    public int HP
    {
        get => _HP;
        set
        {
            if (value == _HP) return;
            _HP = value;
            CEventSystem.Instance.ActivateEvent(EEventType.HPChange, value);
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
            CEventSystem.Instance.ActivateEvent(EEventType.BombChange, value);
        }
    }

    [Header("状态")]

    private float t_shoot = 1f;
    private bool b_canShoot = true;
    private bool b_wantShoot;
    [SerializeField]
    private float offset_shoot = 0.5f;          //炮口到车身中心的距离

    private bool b_wantThrowBomb;

    public float MaxSpeed { get; private set; } = 4f;
    public float DeltaMaxPalstance { get; private set; } = 1.8f;           //车身每固定帧旋转的最大角度
    [SerializeField] private bool _IsMoving;
    public bool IsMoving
    {
        get => _IsMoving;
        set
        {

            _IsMoving = value;
        }
    }
    internal Vector2 drct_move;     //车身的角度
    internal Vector3 m_pos;         //车身的位置

    public float DeltaMaxPalstance_Turret { get; private set; } = 3.6f;    //炮塔每固定帧旋转的最大角度
    internal Vector2 drct_target;   //瞄准的方向
    internal float angle_mouse;     //瞄准的角度
    internal float angle_turret;    //炮塔的角度

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
        HP = 100;
        NumOfBomb = 3;
    }

    private void Update()
    {
        Decide();
    }

    [SerializeField] float angle;
    private void FixedUpdate()
    {
        PhysicsCheck();
        Move();
        ThrowBomb();
        Shoot();
    }

    protected void Decide()
    {
        //待修改
        drct_move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(Input.GetMouseButtonUp(0)) b_wantShoot = true;
        if (Input.GetMouseButtonUp(1)) b_wantThrowBomb = true;

        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        drct_target = v.normalized;
        angle_mouse = CTool.Direction2Angle(drct_target);
    }

    private void PhysicsCheck()
    {
        m_pos = transform.position;
        IsMoving = m_Rigidbody.velocity.magnitude > 0.1f;
    }

    private void Move()
    {
        //待修改
        m_Rigidbody.velocity = drct_move * MaxSpeed;
    }

    private void Shoot()
    {
        void Shoot_(int bulletIndex, Vector3 offset, float angle)
        {
            CDanmakuController.Instance.Shoot(bulletIndex, m_pos + offset, angle);
        }

        if (!b_wantShoot) return;
        b_wantShoot = false;
        if (!b_canShoot) return;
        //待修改
        StartCoroutine(ShootCoolDown());
        Shoot_(1, offset_shoot * drct_target, angle_mouse);
    }
    private IEnumerator ShootCoolDown()
    {
        b_canShoot = false;
        yield return CTool.Wait(t_shoot);
        b_canShoot = true;
    }

    private void ThrowBomb()
    {
        if (!b_wantThrowBomb || NumOfBomb == 0) return;

        b_wantThrowBomb = false;
    }
}
