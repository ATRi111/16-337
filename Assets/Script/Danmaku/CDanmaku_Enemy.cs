using Public;
using UnityEngine;

//���˷���ĵ�Ļ
public class CDanmaku_Enemy : CDanmaku
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable_Player obj = collision.GetComponent<IDamagable_Player>();
        if (obj != null)
        {
            obj.GetDamage(m_damage);
            Recycle();
        }
    }
}
