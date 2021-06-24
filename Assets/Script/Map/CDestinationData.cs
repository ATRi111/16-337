using UnityEngine;
using Public;

//敌人选择目的地的依据
public class CDestinationData : CSingleton<CDestinationData>
{
    public Vector3 pos_beSeen;  //玩家上次被看见的位置
    public Vector3 pos_Shoot;   //玩家上次射击的位置
    public Vector3 b_recetnlyBeSeen;    //true表示玩家最近一次是被看到（而不是听到）

    public Vector3 pos_Base;    //基地位置
}
