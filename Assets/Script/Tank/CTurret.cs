using Public;
using System.Collections;
using UnityEngine;

public class CTurret : MonoBehaviour
{
    internal float m_deltaMaxAngle = 2f;    //每固定帧旋转的最大角度
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

    [SerializeField] protected float m_offset; //炮口到炮塔轴心的距离

    internal float t_shoot = 1f;
    protected bool b_canShoot = true;

    [Header("外部变量")]
    public float angle_deviation;     //射击时角度偏差范围
    public float angle_target;        //想要转向的角度
    internal bool b_wantShoot;

    private void FixedUpdate()
    {
        Angle = CTool.Rotate(Angle, angle_target, m_deltaMaxAngle);
        Shoot();
    }

    protected virtual void Shoot()
    {

        if (!b_wantShoot || !b_canShoot) return;

        bool targetedWithPlayer;    //是否对准玩家
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
