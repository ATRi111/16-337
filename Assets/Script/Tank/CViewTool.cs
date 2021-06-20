using Public;
using UnityEngine;

//视野工具类
public class CViewTool :CSingleton<CViewTool>
{
    public LayerMask obstacleLayer;
    public LayerMask grassLayer;

    public bool BlockedByObstacle(CViewPoint vp1,CViewPoint vp2)
    {
        Vector2 origin = vp1.m_pos;
        Vector2 displacement = vp2.m_pos - vp1.m_pos;
        bool hit = Physics2D.Raycast(origin, displacement.normalized, displacement.magnitude, obstacleLayer);
        //if(!hit) Debug.DrawLine(vp1.m_pos,vp2.m_pos, Color.green);
        return hit;
    }


}
