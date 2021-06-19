using UnityEngine;
using Public;

public class CBody : MonoBehaviour
{
    public float m_maxSpeed = 4f;
    public float t_accelerate = 0.4f;           //����ʱ��
    private float _deltaSpeed_accelerate;
    public float t_friction = 0.4f;             //��Ħ������ʱ��
    private float _deltaSpeed_friction;
    public float t_decelerate = 0.2f;           //����ʱ��
    private float _deltaSpeed_decelerate;

    public float m_deltaMaxAngle = 3.6f;        //ÿ�̶�֡��ת�����Ƕ�

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

    public bool b_isMoving;

    [SerializeField]
    private Rigidbody2D mainRigidbody;
    public float angle_target;    //��Ҫת��ĽǶ�
    public float sign_move;       //1��ʾǰ����-1��ʾ���ˣ�0��ʾ��ǰ��Ҳ������ 

    private void Awake()
    {
        mainRigidbody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        _deltaSpeed_accelerate = m_maxSpeed / t_accelerate * Time.fixedDeltaTime;
        _deltaSpeed_friction = -m_maxSpeed / t_friction * Time.fixedDeltaTime;
        _deltaSpeed_decelerate = -m_maxSpeed / t_decelerate * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
        Move();
    }

    private void Move()
    {
        float deltaSpeed;
        if (sign_move > 0) deltaSpeed = _deltaSpeed_accelerate;
        else if(sign_move <0)  deltaSpeed = _deltaSpeed_decelerate;
        else deltaSpeed = _deltaSpeed_friction;
        mainRigidbody.velocity += deltaSpeed * CTool.Angle2Direction(Angle);

        if (mainRigidbody.velocity.magnitude > m_maxSpeed) mainRigidbody.velocity = m_maxSpeed * mainRigidbody.velocity.normalized;

        b_isMoving = mainRigidbody.velocity.magnitude > 0.1f;
    }
}
