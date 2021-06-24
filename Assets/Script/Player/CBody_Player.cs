using UnityEngine;
using Public;

public class CBody_Player : CBody
{
    internal float m_deltaMaxAngle = 1f;        //每固定帧旋转的最大角度
    [SerializeField] private float _Angle;
    public override float Angle
    {
        get => _Angle;
        set
        {
            _Angle = value;
            transform.eulerAngles = new Vector3(0, 0, 360f - value);
        }
    }

    public float m_maxSpeed = 3f;
    internal float t_accelerate = 1.5f;           //仅动力加速时间
    private float _deltaSpeed_accelerate;
    internal float t_friction = 2f;             //仅摩擦力减速时间
    private float _deltaSpeed_friction;

    [Header("外部变量")]
    public float angle_target;    //想要转向的角度
    public float sign_move;       //1表示前进，-1表示后退，0表示不前进也不后退 
    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        _deltaSpeed_accelerate = m_maxSpeed / t_accelerate * Time.fixedDeltaTime;
        _deltaSpeed_friction = -m_maxSpeed / t_friction * Time.fixedDeltaTime;

        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void FixedUpdate()
    {
        Angle = CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
        Move();
        b_isMoving = m_rigidbody.velocity.magnitude > 0.5f;
    }

    private void Move()
    {
        //动力
        m_rigidbody.velocity += sign_move * _deltaSpeed_accelerate * CTool.Angle2Direction(Angle);
        if (m_rigidbody.velocity.magnitude > m_maxSpeed) 
            m_rigidbody.velocity = m_maxSpeed * m_rigidbody.velocity.normalized;
        //摩擦
        float v = m_rigidbody.velocity.magnitude;
        if (v + _deltaSpeed_friction < 0) 
            v = 0;
        else 
            v += _deltaSpeed_friction;
        m_rigidbody.velocity = v * m_rigidbody.velocity.normalized;

        b_isMoving = m_rigidbody.velocity.magnitude > 1f;
    }
}
