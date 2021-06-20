using UnityEngine;
using Public;

public class CBody : MonoBehaviour
{
    internal float m_deltaMaxAngle = 1f;        //ÿ�̶�֡��ת�����Ƕ�
    [SerializeField] private float _Angle;
    public float Angle
    {
        get => _Angle;
        set
        {
            _Angle = value;
            transform.eulerAngles = new Vector3(0, 0, 360f - value);
        }
    }

    public float m_maxSpeed = 4f;
    internal float t_accelerate = 1.5f;           //����������ʱ��
    private float _deltaSpeed_accelerate;
    internal float t_friction = 2f;             //��Ħ��������ʱ��
    private float _deltaSpeed_friction;

    public bool b_isMoving;

    [Header("�ⲿ����")]
    public float angle_target;    //��Ҫת��ĽǶ�
    public float sign_move;       //1��ʾǰ����-1��ʾ���ˣ�0��ʾ��ǰ��Ҳ������ 
    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        _deltaSpeed_accelerate = m_maxSpeed / t_accelerate * Time.fixedDeltaTime;
        _deltaSpeed_friction = -m_maxSpeed / t_friction * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        Angle = CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
        Move();
    }

    [SerializeField] float ve;
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
        ve = m_rigidbody.velocity.magnitude;
    }
}
