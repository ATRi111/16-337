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

    [SerializeField] private float m_offset; //�ڿڵ��������ĵľ���

    private float t_shoot = 0.5f;
    private bool b_canShoot = true;

    [Header("�ⲿ����")]
    public float angle_deviation;     //���ʱ�Ƕ�ƫ�Χ,��CTank�ű�����
    public float angle_target;        //��Ҫת��ĽǶ�
    internal bool b_wantShoot;

    private void FixedUpdate()
    {
        Angle = CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
        Shoot();
    }

    private void Shoot()
    {
        if (!b_wantShoot) return;
        b_wantShoot = false;
        if (!b_canShoot) return;

        StartCoroutine(ShootCoolDown());
        float shootAngle = Angle + Random.Range(-1f, 1f) * angle_deviation;
        CDanmakuController.Instance.Shoot(1, transform.position + m_offset * (Vector3)CTool.Angle2Direction(shootAngle), shootAngle);
    }
    private IEnumerator ShootCoolDown()
    {
        b_canShoot = false;
        yield return CTool.Wait(t_shoot);
        b_canShoot = true;
    }

}
