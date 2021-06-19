using Public;
using System.Collections;
using UnityEngine;

public class CTank : MonoBehaviour
{
    [Header("ÊôÐÔ")]
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
    protected float angle_deviation_move = 6f;   //ÒÆ¶¯Ê±£¬Éä»÷Ê±½Ç¶ÈÆ«²î·¶Î§
    protected float angle_deviation_idle = 3f;    //¾²Ö¹Ê±£¬Éä»÷Ê±½Ç¶ÈÆ«²î·¶Î§
    
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
