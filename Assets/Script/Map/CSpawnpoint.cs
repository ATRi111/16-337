using System.Collections;
using UnityEngine;

public class CSpawnpoint : MonoBehaviour
{
    private void Start()
    {
        CPlayer.Instance.transform.position = transform.position;
        CDestinationData.Instance.pos_beSeen = CDestinationData.Instance.pos_Shoot = transform.position;
    }
}
