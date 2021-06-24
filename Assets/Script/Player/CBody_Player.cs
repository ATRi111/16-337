using UnityEngine;
using Public;

public class CBody_Player : CBody
{
    internal float m_deltaMaxAngle = 1f;        //ÿ�̶�֡��ת�����Ƕ�
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
    internal float t_accelerate = 1.5f;           //����������ʱ��
    private float _deltaSpeed_accelerate;
    internal float t_friction = 2f;             //��Ħ��������ʱ��
    private float _deltaSpeed_friction;

    [Header("�ⲿ����")]
    public float angle_target;    //��Ҫת��ĽǶ�
    public float sign_move;       //1��ʾǰ����-1��ʾ���ˣ�0��ʾ��ǰ��Ҳ������ 
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
        //����
        m_rigidbody.velocity += sign_move * _deltaSpeed_accelerate * CTool.Angle2Direction(Angle);
        if (m_rigidbody.velocity.magnitude > m_maxSpeed) 
            m_rigidbody.velocity = m_maxSpeed * m_rigidbody.velocity.normalized;
        //Ħ��
        float v = m_rigidbody.velocity.magnitude;
        if (v + _deltaSpeed_friction < 0) 
            v = 0;
        else 
            v += _deltaSpeed_friction;
        m_rigidbody.velocity = v * m_rigidbody.velocity.normalized;

        b_isMoving = m_rigidbody.velocity.magnitude > 1f;
    }
}
