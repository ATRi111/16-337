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
    protected float angle_deviation_move = 6f;   //�ƶ�ʱ�����ʱ�Ƕ�ƫ�Χ
    protected float angle_deviation_idle = 3f;    //��ֹʱ�����ʱ�Ƕ�ƫ�Χ
    
    protected CTurret cTurret;
    protected CBody cBody;

    protected virtual void Awake()
    {
        cTurret = GetComponentInChildren<CTurret>();
        cBody = GetComponentInChildren<CBody>();
    }

    protected void Update()
    {
        Decide();
    }

    protected void FixedUpdate()
    {
        PhysicsCheck();
    }

    protected virtual void Decide()
    {

    }

    protected void PhysicsCheck()
    {
        cTurret.angle_deviation = cBody.b_isMoving ? angle_deviation_move : angle_deviation_idle;
    }
}
