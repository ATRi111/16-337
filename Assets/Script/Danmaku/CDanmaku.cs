using Public;
using UnityEngine;

public class CDanmaku : MonoBehaviour
{
    internal int m_ID = 0;
    [SerializeField] 
    protected int m_damage = 10;
    protected int m_speed = 5;

    protected Rigidbody2D m_rigidbody;

    [SerializeField] private bool _Active;
    public bool Active
    {
        get => _Active;
        private set
        {
            if (value == _Active) return;
            _Active = value;
            gameObject.SetActive(value);
        }
    }

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int ID)
    {
        _Active = true;
        Active = false;
        m_ID = ID;
    }

    public void Activate(Vector3 pos,float angle)
    {
        Active = true;
        transform.position = pos;
        transform.eulerAngles = new Vector3(0, 0, -angle);
        m_rigidbody.velocity = m_speed * CTool.Angle2Direction(angle) ;
    }

    public void Recycle()
    {
        Active = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
