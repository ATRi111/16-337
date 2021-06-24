using Public;
using System.Collections;
using UnityEngine;

public class CTank : MonoBehaviour
{
    [SerializeField] protected int _HP;
    public virtual int HP
    {
        get => _HP;
        set
        {
            if (value == _HP) return;
            _HP = value;
        }
    }

    protected float angle_deviation_move = 6f;    //�ƶ�ʱ�����ʱ�Ƕ�ƫ�Χ
    protected float angle_deviation_idle = 3f;    //��ֹʱ�����ʱ�Ƕ�ƫ�Χ

    internal Vector3 m_pos;   //�����λ��

    protected CTurret cTurret;
    protected CBody cBody;
    protected CViewPointController cViewPointController;
    protected SpriteRenderer[] spriteRenderers;

    protected virtual void Awake()
    {
        cTurret = GetComponentInChildren<CTurret>();
        cBody = GetComponentInChildren<CBody>();
        cViewPointController = GetComponentInChildren<CViewPointController>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (cTurret == null || cBody == null || cViewPointController == null)
        {
            Debug.LogWarning("ȱ�����");
            Destroy(gameObject);
        }
    }

    protected void FixedUpdate()
    {
        PhysicsCheck();
    }

    protected virtual void PhysicsCheck()
    {
        m_pos = cBody.transform.position;
        cTurret.angle_deviation = cBody.b_isMoving ? angle_deviation_move : angle_deviation_idle;
    }
    //ֻ�����See����Ӧ�ñ��ⲿ����
    public bool See(CTank cTank)
    {
        return cViewPointController.See(cTank.cViewPointController);
    }

    protected virtual void Die()
    {

    }
}
