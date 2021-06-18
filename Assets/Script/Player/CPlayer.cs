using Public;
using System.Collections;
using UnityEngine;

public class CPlayer : CSigleton<CPlayer>
{             
    [Header("����")]
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

    [SerializeField] private int _Power;  //����
    public int Power
    {
        get => _Power;
        set
        {
            if (value == _Power) return;
            _Power = value;
            CEventSystem.Instance.ActivateEvent(EEventType.PowerChange, value);
        }
    }

    [Header("״̬")]

    private float t_shoot = 0.5f;
    private bool b_canShoot = true;
    private bool b_wantShoot;
    [SerializeField]
    private float offset_shoot = 0.5f;          //����㵽���ĵľ���
    /*
    private float angle_deviation_high = 10f;   //�����ƶ��£����ʱ�Ƕ�ƫ�Χ
    private float angle_deviation_low = 5f;     //�����ƶ��£����ʱ�Ƕ�ƫ�Χ
    private float angle_deviation_idle = 1f;    //��ֹʱ�����ʱ�Ƕ�ƫ�Χ
    */
    private bool b_wantThrowBomb;

    public float MaxSpeed { get; private set; } = 4f;
    public float DeltaMaxPalstance { get; private set; } = 1.8f;           //����ÿ�̶�֡��ת�����Ƕ�
    internal bool b_isMoving;
    internal Vector2 drct_move;
    internal Vector3 m_pos;

    public float DeltaMaxPalstance_Turret { get; private set; } = 3.6f;    //����ÿ�̶�֡��ת�����Ƕ�
    internal Vector2 drct_mouse;
    internal float angle_mouse;
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
        //���޸�
        drct_move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(Input.GetMouseButtonUp(0)) b_wantShoot = true;
        if (Input.GetMouseButtonUp(1)) b_wantThrowBomb = true;

        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        drct_mouse = v.normalized;
        angle_mouse = CTool.Direction2Angle(drct_mouse);
    }

    private void PhysicsCheck()
    {
        m_pos = transform.position;
        b_isMoving = m_Rigidbody.velocity.magnitude > 0.1f;
    }

    private void Move()
    {
        //���޸�
        m_Rigidbody.velocity = drct_move * MaxSpeed;
    }

    private void Shoot()
    {
        void Shoot_(int bulletIndex, Vector3 offset, float angle)
        {
            CBulletController.Instance.Shoot(bulletIndex, m_pos + offset, angle);
        }

        if (!b_wantShoot) return;
        b_wantShoot = false;
        if (!b_canShoot) return;

        StartCoroutine(ShootCoolDown());
        Shoot_(0, offset_shoot * drct_mouse, angle_mouse);
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
