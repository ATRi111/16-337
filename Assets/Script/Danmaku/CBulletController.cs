using Public;
using UnityEngine;

public class CBulletController : CSigleton<CBulletController>
{
    public GameObject[] danmakus;   //弹幕样本
    public int[] sizes;             //弹幕池中每种子弹的数量
    private CDanmakuPool[] cPools;  //弹幕池
    private int m_size;

    private void Start()
    {
        m_size = sizes.Length;
        cPools = new CDanmakuPool[m_size];
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < m_size; i++)
        {
            //创建子弹池
            GameObject pool = new GameObject("Pool" + i.ToString());
            pool.transform.parent = gameObject.transform;
            //创建脚本
            try
            {
                CDanmakuPool cDanmakuPool = pool.AddComponent<CDanmakuPool>();
                cDanmakuPool.Initialize(i, sizes[i], danmakus[i]);
                cPools[i] = cDanmakuPool;
            }
            catch
            {
                Debug.LogWarning("创建失败");
            }
        }
    }

    public void Shoot(int index, Vector3 pos, float angle = 0)
    {
        Debug.Log("shoot");
        cPools[index].Shoot(pos, angle);
    }
}
