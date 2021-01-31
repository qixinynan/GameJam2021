using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchItem : MonoBehaviour
{
    public int id;
    public List<Sprite> spList;
    public SpriteRenderer sr;

    private AudioSource audioSource;

    private void Start()
    {

        AudioInit();
        StartCoroutine(Util.DelayExecute(() =>
        {
            return GameController.manager.doorMan.initFinish;
        }, () =>
        {
            SwitchInfo info = GameController.manager.doorMan.GetSwitchInfoById(id);
            Debug.Log(info);
            if (info == null)
            {
                return;
            }
            Debug.Log(info.colorType);
            sr.sprite = spList[(int) info.colorType];
            sr.transform.localPosition = new Vector3(sr.transform.localPosition.x, sr.transform.localPosition.y, -2.0f);
        }));
    }

    private void AudioInit()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
        else if(audioSource.clip == null)
        {
            Debug.LogError("Audio Source's Clip is Null");
        }
        else
        {
            
        }
    }

    public void SwitchType()
    {
        SwitchInfo info = GameController.manager.doorMan.GetSwitchInfoById(id);
        if (info == null)
        {
            return;
        }
        info.SwitchType();
        sr.sprite = spList[(int)info.colorType];
        audioSource.Play();
    }
    
}
