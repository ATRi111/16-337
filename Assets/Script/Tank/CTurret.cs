using Public;
using System.Collections;
using UnityEngine;

public class CTurret : MonoBehaviour
{
    internal float m_deltaMaxAngle = 2f;    //ÿ�̶�֡��ת�����Ƕ�
    [SerializeField] private float _Angle;
    public float Angle
    {
        get => _Angle;
        set
        {
            _Angle = value;
            transform.eulerAngles = new Vector3(0, 0, 360f - value);
        }
    }

    [SerializeField] protected float m_offset; //�ڿڵ��������ĵľ���

    internal float t_shoot = 1f;
    protected bool b_canShoot = true;

    [Header("�ⲿ����")]
    public float angle_deviation;     //���ʱ�Ƕ�ƫ�Χ
    public float angle_target;        //��Ҫת��ĽǶ�
    internal bool b_wantShoot;

    private void FixedUpdate()
    {
        Angle = CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
        Shoot();
    }

    protected virtual void Shoot()
    {

        if (!b_wantShoot || !b_canShoot) return;

        bool targetedWithPlayer;    //�Ƿ��׼���
        targetedWithPlayer = Mathf.Abs(CTool.Direction2Angle(CPlayer.Instance.m_pos - transform.position) - Angle) < 10f;
        if (!targetedWithPlayer) return;

        StartCoroutine(ShootCoolDown());
        float shootAngle = Angle + Random.Range(-1f, 1f) * angle_deviation;
        CDanmakuController.Instance.Shoot(1, transform.position + m_offset * (Vector3)CTool.Angle2Direction(shootAngle), shootAngle);
    }
    protected IEnumerator ShootCoolDown()
    {
        b_canShoot = false;
        yield return CTool.Wait(t_shoot);
        b_canShoot = true;
    }

}
