using UnityEngine;

public class Cheater : Public.CSigleton<Cheater>
{
    private void Update()
    {
        if (CPlayer.Instance != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) CPlayer.Instance.Power = 100;
            if (Input.GetKeyDown(KeyCode.Alpha2)) CPlayer.Instance.Power = 200;
            if (Input.GetKeyDown(KeyCode.Alpha3)) CPlayer.Instance.Power = 300;
            if (Input.GetKeyDown(KeyCode.Alpha4)) CPlayer.Instance.Power = 400;
            if (Input.GetKeyDown(KeyCode.Alpha5)) CPlayer.Instance.Power = 500;
            if (Input.GetKeyDown(KeyCode.Alpha6)) CPlayer.Instance.HP++;
            if (Input.GetKeyDown(KeyCode.Alpha7)) CPlayer.Instance.NumOfBomb++;
        }
    }
}
