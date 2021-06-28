using Public;
using System.Collections;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

public class CSceneManager : CSingleton<CSceneManager>
{
    private const int MAXINDEX = 1;
    //��ǰ�ص�index
    [SerializeField] private int _Index;

    internal int Index
    {
        get =>_Index;
        private set
        {
            if (value > MAXINDEX || value < 0)
                value = 0;
            if (value == _Index)
                return;

            StartCoroutine(ILoadLevel(value));
            _Index = value;
            CEventSystem.Instance.ActivateEvent(EEventType.SceneLoad, value);
        }
    }

    private void Start()
    {
        _Index = 0;
    }

    //��ֹ�ò����ڱ���ķ������س���
    public void LoadLevel(int index)
    {
        Index = index;
    }
    public void LoadNextLevel()
    {
        Index++;
    }
    //ֱ���������Э���ǲ���ȫ��
    private IEnumerator ILoadLevel(int index)
    {
        AsyncOperation Async_LoadScene = LoadSceneAsync(index);
        Async_LoadScene.allowSceneActivation = true;
        switch(index)
        {
            default:
                break;
        }
        yield return null;
    }

    public void Exit()
    {
        Index = 0;
    }
    //����������˳���Ϸ
    public void Quit()
    {
        Application.Quit();
    }
}