using UnityEngine;

public class Boarder : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        CDanmaku danmaku = collision.GetComponent<CDanmaku>();
        if (danmaku != null)
        {
            danmaku.Recycle();
        }
    }
}
