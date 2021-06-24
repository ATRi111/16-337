using Public;
using UnityEngine;

public class CTurret_Player : CTurret
{
    protected override void Shoot()
    {
        if (!b_wantShoot) return;
        b_wantShoot = false;
        if (!b_canShoot) return;

        StartCoroutine(ShootCoolDown());
        float shootAngle = Angle + Random.Range(-1f, 1f) * angle_deviation;
        CDanmakuController.Instance.Shoot(1, transform.position + m_offset * (Vector3)CTool.Angle2Direction(shootAngle), shootAngle);
        CDestinationData.Instance.pos_Shoot = transform.position;
    }
}
