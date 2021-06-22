using System.Collections;
using UnityEngine;

public class CSpawnpoint : MonoBehaviour
{

    private void OnEnable()
    {
        CEventSystem.Instance.AddLisenter<int>(EEventType.SceneLoad, Spawn);
    }
    private void OnDisable()
    {
        CEventSystem.Instance.RemoveListener<int>(EEventType.SceneLoad, Spawn);
    }
    public void Spawn(int index)
    {
        StartCoroutine(WaitForPlayer());
        CPlayer.Instance.transform.position = transform.position;
    }
    private IEnumerator WaitForPlayer()
    {
        for(float counter=0f;CPlayer.Instance == null && counter<1f; counter += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }
    }
}
