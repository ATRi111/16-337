using Public;
using System.Collections;
using UnityEngine;

public class CEnemy : CTank, IDamagable
{
    protected Vector3 m_Pos;

    private void Start()
    {
        StartCoroutine(SlowUpdate());
    }

    protected virtual void Update()
    {
        m_Pos = transform.position;
    }

    //ÿ0.1s����һ��,�������ϵ������������
    private IEnumerator SlowUpdate()
    {
        for (; ; )
        {
            Shoot();
            yield return CTool.Wait(0.1f);
        }
    }

    protected virtual void Shoot_(int damakuIndex, Vector3 offset, int angle)
    {
        CDanmakuController.Instance.Shoot(damakuIndex, m_Pos + offset, angle);
    }
    protected virtual void Shoot()
    {

    }

    public void GetDamage(int damage)
    {
        HP -= damage;
    }
}
