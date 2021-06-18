using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDanmakuPool : MonoBehaviour
{
    public int m_ID;
    public int m_size;
    public CDanmaku[] cDanmakus;
    private int NextIndex;  //�´β����ӵ�ʱ������±꿪ʼ������ȫ�ɿ�

    public void Initialize(int ID, int size, GameObject sample)
    {
        m_ID = ID;
        m_size = size;
        if (sample.GetComponent<CDanmaku>() == null)
        {
            Debug.LogWarning("δ���ص�Ļ�ű�");
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
            //�����ӵ�
            GameObject bullet = GameObject.Instantiate(sample);
            bullet.transform.parent = gameObject.transform;
            //��ȡ�ű�
            cDanmakus[i] = bullet.GetComponent<CDanmaku>();
            cDanmakus[i].Initialize(i);
            yield return null;
        }
        GameManager.Instance.NumOfLoadAsync--;
    }
    //CBulletPoolController������಻Ӧ�õ����������
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
        Debug.LogWarning("���еĵ�Ļ������");
    }
}
