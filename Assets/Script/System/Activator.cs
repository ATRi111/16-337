using System.Collections.Generic;
using UnityEngine;

//����Ҫ��˳�򼤻����Ϸ��������������б���
public class Activator : MonoBehaviour
{
    [SerializeField] private List<GameObject> list = new List<GameObject>();

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Second);
        foreach (GameObject obj in list)
        {
            obj.SetActive(true);
        }
        Destroy(gameObject);
    }
}
