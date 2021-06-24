using Public;
using System.Collections;
using UnityEngine;

public class CEnemy : CTank
{
    [SerializeField] private bool _Visible;
    public bool Visible
    {
        get => _Visible;
        set
        {
            if (value == _Visible) return;
            _Visible = value;
            foreach(SpriteRenderer item in spriteRenderers)
            {
                item.enabled = value;
            }
        }
    }
    private bool _SeePlayer;
    public bool SeePlayer
    {
        get => _SeePlayer;
        set
        {
            _SeePlayer = value;
            cTurret.b_wantShoot = value;
            if (value)
            {
                CDestinationData.Instance.pos_beSeen = CPlayer.Instance.m_pos;
            }
        }
    }

    private PolyNavAgent navi;  //µ¼º½×é¼þ

    protected override void Awake()
    {
        base.Awake();
        _Visible = true;
        Visible = false;
        navi = GetComponent<PolyNavAgent>();
    }

    protected override void PhysicsCheck()
    {
        base.PhysicsCheck();
        Visible = CPlayer.Instance.See(this);
        SeePlayer = See(CPlayer.Instance);
    }

    protected override void Decide()
    {
        
    }
}
