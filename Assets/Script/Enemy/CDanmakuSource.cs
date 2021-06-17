using Public;
using System.Collections;
using UnityEngine;

public class CDanmakuSource : CEnemy
{
    private int m_angle;

    private void Awake()
    {
        m_angle = 0;
    }

    private void Start()
    {
        StartCoroutine(Rotate());
        StartCoroutine(Attack1());
    }

    private IEnumerator Rotate()
    {
        for (; ; )
        {
            m_angle += 5;
            yield return CTool.Wait(0.1f);
        }
    }

    protected override void Shoot_(int damakuIndex, Vector3 offset, int angle)
    {
        CDanmakuController.Instance.Shoot(damakuIndex, m_Pos + offset, m_angle + angle);
    }
    //向四面八方发射直线弹幕
    private IEnumerator Attack1()
    {
        for (; ; )
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Shoot_(0, CTool.s_zeroVector, 30 * j);
                }
                yield return CTool.Wait(0.1f);
            }
            yield return CTool.Wait(0.5f);
        }
    }
}