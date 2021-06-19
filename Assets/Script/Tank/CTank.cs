using Public;
using System.Collections;
using UnityEngine;

public class CTank : MonoBehaviour
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

    internal Vector3 m_pos;
           
    private float angle_deviation_move = 10f;   //�ƶ�ʱ�����ʱ�Ƕ�ƫ�Χ
    private float angle_deviation_idle = 1f;    //��ֹʱ�����ʱ�Ƕ�ƫ�Χ
    
    protected CTurret cTurret;
    protected CBody cBody;

    protected virtual void Awake()
    {
        cTurret = GetComponentInChildren<CTurret>();
        cBody = GetComponentInChildren<CBody>();
    }

    private void Update()
    {
        Decide();
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
    }

    protected virtual void Decide()
    {

    }

    private void PhysicsCheck()
    {
        m_pos = transform.position;
        cTurret.angle_deviation = cBody.b_isMoving ? angle_deviation_move : angle_deviation_idle;
    }
}
