using Public;
using System.Collections;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

public class CSceneManager : CSingleton<CSceneManager>
{
    private const int MAXINDEX = 1;
    //当前关的index
    [SerializeField] private int _Index;
    [SerializeField] private GameObject player;

    internal int Index
    {
        get
        {
            return _Index;
        }
        set
        {
            if (value > MAXINDEX || value < 0)
                value = 0;
            if (value == _Index)
                return;

            StartCoroutine(ILoadLevel(value));
            _Index = value;
            CEventSystem.Instance.ActivateEvent(EEventType.SceneLoaded, value);
        }
    }

    //禁止用不属于本类的方法加载场景
    public void LoadLevel(int index)
    {
        Index = index;
    }
    public void LoadNextLevel()
    {
        Index++;
    }
    //直接启用这个协程是不安全的
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
    //必须从这里退出游戏
    public void Quit()
    {
        Application.Quit();
    }
}

