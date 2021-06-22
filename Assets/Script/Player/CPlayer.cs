using Public;
using System.Collections;
using UnityEngine;

public class CPlayer : CTank
{
    public int maxHP;
    public override int HP
    {
        get => _HP;
        set
        {
            if (value == _HP) return;
            _HP = value;
            CEventSystem.Instance.ActivateEvent(EEventType.HPChange, value , maxHP);
        }
    }

    public static CPlayer Instance { get; private set; }
    private CBody_Player cBody_Player;
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        cBody_Player = GetComponentInChildren<CBody_Player>();
    }

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        maxHP = HP = 100;
    }

    protected override void Decide()
    {
        cBody_Player.sign_move = Input.GetAxisRaw("Vertical");
        cBody_Player.angle_target = cBody_Player.Angle + Input.GetAxisRaw("Horizontal") * 90f;
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cBody_Player.transform.position;
        Vector2 v2 = v3.normalized;
        cTurret.angle_target = CTool.Direction2Angle(v2);

        if (Input.GetMouseButtonUp(0)) cTurret.b_wantShoot = true;
    }
}
