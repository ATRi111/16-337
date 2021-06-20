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

    protected override void Awake()
    {
        base.Awake();
        _Visible = true;
        Visible = false;
    }

    protected override void PhysicsCheck()
    {
        base.PhysicsCheck();
        Visible = CPlayer.Instance.See(this);
    }

    protected override void Decide()
    {
        
    }

}
