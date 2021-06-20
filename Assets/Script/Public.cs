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

        //��������Ϊ����ı�ʾ���峯��ͷ��䷽��ĽǶȶ���ѭ���¹��򣺳���Ϊ0�㣬˳ʱ��Ϊ�Ƕ�����ķ��򣬲�����360��
        public static Vector2 Angle2Direction(float angle)
            => new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        public static float Direction2Angle(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f;
            return angle;
        }
        //��originAngle��targetAngle��ת������maxAngle
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
            
    //���˺���λ�Ľӿ�
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
                if (Instance == null) Debug.Log("��������ʧ��");
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}
