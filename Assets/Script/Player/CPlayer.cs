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

    [Header("״̬")]

    private float t_shoot = 1f;
    private bool b_canShoot = true;
    private bool b_wantShoot;
    [SerializeField]
    private float offset_shoot = 0.5f;          //�ڿڵ��������ĵľ���

    private bool b_wantThrowBomb;

    public float MaxSpeed { get; private set; } = 4f;
    public float DeltaMaxPalstance { get; private set; } = 1.8f;           //����ÿ�̶�֡��ת�����Ƕ�
    [SerializeField] private bool _IsMoving;
    public bool IsMoving
    {
        get => _IsMoving;
        set
        {

            _IsMoving = value;
        }
    }
    internal Vector2 drct_move;     //����ĽǶ�
    internal Vector3 m_pos;         //�����λ��

    public float DeltaMaxPalstance_Turret { get; private set; } = 3.6f;    //����ÿ�̶�֡��ת�����Ƕ�
    internal Vector2 drct_target;   //��׼�ķ���
    internal float angle_mouse;     //��׼�ĽǶ�
    internal float angle_turret;    //�����ĽǶ�

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
        //���޸�
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
        //���޸�
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
        //���޸�
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
