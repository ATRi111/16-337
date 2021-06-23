using System.Collections;
using UnityEngine;

public class CSpawnpoint : MonoBehaviour
{
    private void Awake()
    {
        CPlayer.Instance.transform.position = transform.position;
    }
}
