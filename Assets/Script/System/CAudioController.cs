using Public;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
    Move,Shoot
}

public class CAudioController : CSingleton<CAudioController>
{
    [SerializeField]
    private AudioSource[] AudioSources;
    private Dictionary<ESound, AudioSource> m_AudioDict = new Dictionary<ESound, AudioSource>();

    protected override void Awake()
    {
        base.Awake();
        AudioSources = GetComponentsInChildren<AudioSource>();
        BuildDict();
    }


    private void BuildDict()
    {
        AudioSource FindSound(string name)
        {
            foreach (AudioSource item in AudioSources)
            {
                if (item.gameObject.name == name)
                    return item;
            }
            Debug.LogWarning("找不到名为" + name + "的音效");
            return null;
        }

        m_AudioDict.Add(ESound.Move, FindSound("fx_move"));
        m_AudioDict.Add(ESound.Shoot, FindSound("fx_shoot"));
    }

    public void PlaySound(ESound ename)
    {
        AudioSource audio = m_AudioDict[ename];
        if (audio == null)
            return;
        m_AudioDict[ename].Play();
    }
    public void PlaySoundLoop(ESound ename)
    {
        AudioSource audio = m_AudioDict[ename];
        if (audio == null)
            return;
        m_AudioDict[ename].loop = true;
        m_AudioDict[ename].Play();
    }
    public void StopSound(ESound ename)
    {
        AudioSource audio = m_AudioDict[ename];
        if (audio == null)
            return;
        m_AudioDict[ename].Stop();
    }
    public void StopAllsounds()
    {
        foreach (AudioSource item in AudioSources)
        {
            item.Stop();
        }
    }

    private void Update()
    {
        transform.position = CPlayer.Instance.transform.position;
    }
}
