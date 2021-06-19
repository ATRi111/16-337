using Public;
using System.Collections;
using UnityEngine;

public class CTurret : MonoBehaviour
{
    public float m_deltaMaxAngle = 3.6f;    //每固定帧旋转的最大角度
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

    [SerializeField] private float m_offset = 0.5f; //炮口到炮塔轴心的距离

    private float t_shoot = 0.5f;
    private bool b_canShoot = true;
    internal bool b_wantShoot;
    public float angle_deviation;     //射击时角度偏差范围,由CTank脚本控制
    public float angle_target;        //想要转向的角度

    private void FixedUpdate()
    {
        CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
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
