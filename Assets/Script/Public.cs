using System.Collections;
using System.Collections.Generic;
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

        //代码中人为定义的表示物体朝向和发射方向的角度都遵循以下规则：朝上为0°，顺时针为角度增大的方向，不超过360°
        public static Vector2 Angle2Direction(float angle)
            => new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        public static float Direction2Angle(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f;
            return angle;
        }
        //从originAngle向targetAngle旋转不超过maxAngle
        public static float Rotate(float originAngle,float targetAngle, float maxAngle =360f)
        {
            targetAngle = (targetAngle + 360f) % 360f;
            originAngle = (originAngle + 360f) % 360f;
            float newAngle;
            float deltaAngle = targetAngle - originAngle;
            if (deltaAngle > 0)
            {
                if (deltaAngle < 180f)
                    newAngle = Mathf.Min(maxAngle, deltaAngle);
                else
                    newAngle = -Mathf.Min(maxAngle, 360f - deltaAngle);
            }
            else
            {
                deltaAngle = -deltaAngle;
                if (deltaAngle < 180f)
                    newAngle = -Mathf.Min(maxAngle, deltaAngle);
                else
                    newAngle = Mathf.Min(maxAngle, 360f - deltaAngle);
            }

            newAngle += originAngle;
            newAngle = (newAngle + 360f) % 360f;
            return newAngle;
        }
    }
            
    //可伤害单位的接口
    public interface IDamagable
    {
        void GetDamage(int damage);
    }
    public class CSingleton<T> : MonoBehaviour where T : CSingleton<T>
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (Instance == null) Debug.Log("创建单例失败");
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}
