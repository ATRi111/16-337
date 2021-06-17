using Public;
using UnityEngine;

public class CDanmaku : MonoBehaviour
{
    internal int m_ID = 0;
    [SerializeField] 
    protected int m_damage = 10;
    protected int m_speed = 5;
    public float m_angle = 0;

    protected bool _Active;
    public bool Active
    {
        get => _Active;
        private set
        {
            if (value == _Active) return;
            _Active = value;
            if(value)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Initialize(int ID)
    {
        _Active = true;
        Active = false;
        m_ID = ID;
    }

    public void Activate(Vector3 pos,int angle)
    {
        Active = true;
        transform.position = pos;
        transform.eulerAngles = new Vector3(0, 0, angle);
        m_angle = angle;
    }
    public void Recycle()
    {
        Active = false;
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector2 offset = m_speed * Time.fixedDeltaTime * CTool.Angle2Direction(m_angle);
        transform.position += new Vector3(offset.x, offset.y); 
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
