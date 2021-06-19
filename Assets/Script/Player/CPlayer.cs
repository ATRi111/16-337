using Public;
using System.Collections;
using UnityEngine;

public class CPlayer : CTank
{
    public static CPlayer Instance { get; private set; }

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
    }

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        HP = 100;
    }

    protected override void Decide()
    {
        cBody.sign_move = Input.GetAxisRaw("Vertical");
        cBody.angle_target = cBody.Angle + Input.GetAxisRaw("Horizontal") * 90f;
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cBody.transform.position;
        Vector2 v2 = v3.normalized;
        cTurret.angle_target = CTool.Direction2Angle(v2);

        if (Input.GetMouseButtonUp(0)) cTurret.b_wantShoot = true;
    }
}
