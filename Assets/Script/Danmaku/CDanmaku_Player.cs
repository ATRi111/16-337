using Public;
using UnityEngine;


//Íæ¼Ò·¢ÉäµÄµ¯Ä»
public class CDanmaku_Player : CDanmaku
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable obj = collision.GetComponent<IDamagable>();
        if (obj != null)
        {
            obj.GetDamage(m_damage);
            Recycle();
        }
    }
}
