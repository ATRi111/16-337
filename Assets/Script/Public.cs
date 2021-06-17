using System.Collections;
using UnityEngine;

namespace Public
{
    public static class CTool
    {
        public static Quaternion s_zeroQuaternion = new Quaternion();
        public static Vector3 s_zeroVector = Vector3.zero;
        public static GameObject FindFromUI(string name) => GameObject.Find("UI").transform.Find(name).gameObject;
        public static IEnumerator Wait(float duration)
        {
            for (float timer = 0; timer < duration; timer += Time.deltaTime)
                yield return null;
        }
        public static Vector2 RandomVector2()
            => new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        public static Vector3 RandomVector3()
            => new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        public static Vector2 Angle2Direction(float angle)
        => new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        public static float Direction2Angle(Vector2 direction)
            => Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
    //可伤害单位的接口
    public interface IDamagable
    {
        void GetDamage(int damage);
    }
    //友方可伤害单位的接口
    public interface IDamagable_Player
    {
        void GetDamage(int damage);
    }
    public class CSigleton<T> : MonoBehaviour where T : CSigleton<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            try
            {
                Instance = (T)this;
            }
            catch
            {
                Debug.LogWarning("创建单例失败");
            }
            DontDestroyOnLoad(gameObject);
        }
    }

}
