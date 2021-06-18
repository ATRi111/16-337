using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDanmakuPool : MonoBehaviour
{
    public int m_ID;
    public int m_size;
    public CDanmaku[] cDanmakus;
    private int NextIndex;  //下次查找子弹时从这个下标开始，不完全可靠

    public void Initialize(int ID, int size, GameObject sample)
    {
        m_ID = ID;
        m_size = size;
        if (sample.GetComponent<CDanmaku>() == null)
        {
            Debug.LogWarning("未挂载弹幕脚本");
            return;
        }
        cDanmakus = new CDanmaku[size];
        StartCoroutine(GenerateDanmaku(size, sample));
    }
    private IEnumerator GenerateDanmaku(int size, GameObject sample)
    {
        GameManager.Instance.NumOfLoadAsync++;
        for (int i = 0; i < size; i++)
        {
            //创建子弹
            GameObject bullet = GameObject.Instantiate(sample);
            bullet.transform.parent = gameObject.transform;
            //获取脚本
            cDanmakus[i] = bullet.GetComponent<CDanmaku>();
            cDanmakus[i].Initialize(i);
            yield return null;
        }
        GameManager.Instance.NumOfLoadAsync--;
    }
    //CBulletPoolController以外的类不应该调用这个方法
    public void Shoot(Vector3 pos,float angle)
    {
        for(int i = NextIndex; i<m_size;i++)
        {
            if (!cDanmakus[i].Active)
            {
                cDanmakus[i].Activate(pos, angle);
                NextIndex = (i + 1) % m_size;
                return;
            }
        }
        for (int i = 0; i < NextIndex; i++)
        {
            if (!cDanmakus[i].Active)
            {
                cDanmakus[i].Activate(pos, angle);
                NextIndex = (i + 1) % m_size;
                return;
            }
        }
        Debug.LogWarning("池中的弹幕用完了");
    }
}
