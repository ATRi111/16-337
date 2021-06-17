using UnityEngine;
using Public;
using System.Collections;

public class CPlayer : CSigleton<CPlayer>
{
    public float m_speed_high = 4f;
    public float m_speed_low = 2f;

    [Header("����")]
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

    [SerializeField] private int _Power;  //����
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

    [Header("״̬")]
    [SerializeField] private bool _InSlowMode;    //����ģʽ
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

    internal bool b_isShoot;
    internal bool b_WantCastSpellCard;
    internal Vector2 drct_Move;
    internal Vector3 m_Pos;

    private Rigidbody2D m_Rigidbody;

    protected override void Awake()
    {
        base.Awake();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(SlowUpdate());
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
    }
    //ÿ0.1s����һ��
    private IEnumerator SlowUpdate()
    {
        for(; ; )
        {
            Shoot();
            yield return CTool.Wait(0.1f);
        }
    }
    private void InputCheck()
    {
        drct_Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        InSlowMode = Input.GetKey(KeyCode.LeftShift);
        b_isShoot = Input.GetKey(KeyCode.Z);
        if (Input.GetKeyDown(KeyCode.X)) b_WantCastSpellCard = true;
    }

    private void PhysicsCheck()
    {
        m_Pos = transform.position;
    }

    private void Move()
    {
        m_Rigidbody.velocity = drct_Move * (InSlowMode ? m_speed_low : m_speed_high);
    }

    private void Shoot()
    {
        void Shoot_(int bulletIndex, Vector3 offset, int angle)
        {
            CBulletController.Instance.Shoot(bulletIndex, m_Pos + offset, angle);
        }

    }

    private void ThrowBomb()
    {
        if (b_WantCastSpellCard && NumOfBomb > 0)
        {
            NumOfBomb--;

            b_WantCastSpellCard = false;
        }
    }
}
