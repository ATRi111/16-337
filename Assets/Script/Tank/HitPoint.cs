using Public;
using UnityEngine;

public class HitPoint : MonoBehaviour,IDamagable
{
    private CTank cTank;

    private void Awake()
    {
        cTank = GetComponentInParent<CTank>();
        if(cTank == null)
        {
            Debug.Log("’“≤ªµΩCTankΩ≈±æ");
            Destroy(this);
        }
    }

    public void GetDamage(int damage)
    {
        cTank.HP -= damage;
    }
}
