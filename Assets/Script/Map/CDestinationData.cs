using UnityEngine;
using Public;

//����ѡ��Ŀ�ĵص�����
public class CDestinationData : CSingleton<CDestinationData>
{
    public Vector3 pos_beSeen;  //����ϴα�������λ��
    public Vector3 pos_Shoot;   //����ϴ������λ��
    public Vector3 b_recetnlyBeSeen;    //true��ʾ������һ���Ǳ�������������������

    public Vector3 pos_Base;    //����λ��
}
