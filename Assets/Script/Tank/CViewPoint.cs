using UnityEngine;

//同时用作看其他坦克的点和被其他坦克看的点
public class CViewPoint : MonoBehaviour
{
    public float m_view;      //视野

    public bool b_InGrass;
    internal Vector3 m_pos;

    private CBody cBody;
    private CircleCollider2D m_circleCollider;

    private void Awake()
    {
        cBody = GetComponentInParent<CBody>();
        m_circleCollider = GetComponent<CircleCollider2D>();
        if(cBody ==null || m_circleCollider == null)
        {
            Debug.LogWarning("缺少组件");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        m_pos = transform.position;
        b_InGrass = Physics2D.IsTouchingLayers(m_circleCollider, CViewTool.Instance.grassLayer);
    }

    public bool See(CViewPoint vp)
    {
        float visibility = 1;   //vp对于this的可见度
        if (vp.cBody.b_isMoving) visibility *= 1.5f;
        if (vp.b_InGrass) visibility *= 0.5f;
        bool see = (m_pos - vp.m_pos).magnitude < m_view * visibility && !CViewTool.Instance.BlockedByObstacle(this, vp);
        if (see) Debug.DrawLine(m_pos, vp.m_pos, Color.blue);
        return see;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.1f);
        Gizmos.DrawWireSphere(transform.position, m_view);
    }

}
