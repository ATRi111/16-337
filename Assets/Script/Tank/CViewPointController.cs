using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CViewPointController : MonoBehaviour
{
    private CViewPoint[] cViewPoints;

    private void Awake()
    {
        cViewPoints = GetComponentsInChildren<CViewPoint>();
    }

    public bool See(CViewPointController vpc)
    {
        for (int i = 0; i < cViewPoints.Length; i++)
        {
            for (int j = 0; j < vpc.cViewPoints.Length; j++)
            {
                if (cViewPoints[i].See(vpc.cViewPoints[j]))
                    return true;
            }
        }
        return false;
    }
}
