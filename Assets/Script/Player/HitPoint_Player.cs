using Public;
using UnityEngine;

public class HitPoint_Player : MonoBehaviour,IDamagable_Player
{
    private bool _Visible;
    public bool Visible
    {
        get => _Visible;
        set
        {
            if (value == _Visible) return;
            _Visible = value;
            m_spriteRenderer.enabled = value;
        }
    }

    private SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        CEventSystem.Instance.AddLisenter<bool>(EEventType.SlowModeOn, OnSlowModeOn);
    }
    private void OnDisable()
    {
        CEventSystem.Instance.RemoveListener<bool>(EEventType.SlowModeOn, OnSlowModeOn);
    }
    void OnSlowModeOn(bool on)
    {
        Visible = on;
    }

    public void GetDamage(int damage)
    {
        CPlayer.Instance.HP -= damage;
    }
}
