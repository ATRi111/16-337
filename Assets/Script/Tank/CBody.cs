using Public;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBody : MonoBehaviour
{
    public virtual float Angle
    {
        get => 360f -transform.eulerAngles.z;
        set
        {
            return;
        }
    }

    public bool b_isMoving;
    private Vector3 m_pos;

    protected virtual void FixedUpdate()
    {
        b_isMoving = (transform.position - m_pos).magnitude > 0.01f;
    }
}
