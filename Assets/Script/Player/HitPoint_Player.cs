using Public;
using UnityEngine;

public class HitPoint_Player : MonoBehaviour,IDamagable_Player
{
    public void GetDamage(int damage)
    {
        CPlayer.Instance.HP -= damage;
    }
}
