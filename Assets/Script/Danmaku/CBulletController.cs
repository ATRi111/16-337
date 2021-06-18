using Public;
using UnityEngine;

public class CBulletController : CSigleton<CBulletController>
{
    public GameObject[] danmakus;   //��Ļ����
    public int[] sizes;             //��Ļ����ÿ���ӵ�������
    private CDanmakuPool[] cPools;  //��Ļ��
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
            //�����ӵ���
            GameObject pool = new GameObject("Pool" + i.ToString());
            pool.transform.parent = gameObject.transform;
            //�����ű�
            try
            {
                CDanmakuPool cDanmakuPool = pool.AddComponent<CDanmakuPool>();
                cDanmakuPool.Initialize(i, sizes[i], danmakus[i]);
                cPools[i] = cDanmakuPool;
            }
            catch
            {
                Debug.LogWarning("����ʧ��");
            }
        }
    }

    public void Shoot(int index, Vector3 pos, float angle = 0)
    {
        Debug.Log("shoot");
        cPools[index].Shoot(pos, angle);
    }
}
