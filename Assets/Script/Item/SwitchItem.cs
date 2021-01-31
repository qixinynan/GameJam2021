using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchItem : MonoBehaviour
{
    public int id;
    public List<Sprite> spList;
    public SpriteRenderer sr;

    private void Start()
    {
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
        }));
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
    }
    
}
