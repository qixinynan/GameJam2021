using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : MonoBehaviour
{
    public int id;
    public Transform showPosTrans;
    public SpriteRenderer sr;
    public List<Sprite> spList;
    
    private void Start()
    {
        if (sr != null)
        {
            StartCoroutine(Util.DelayExecute(() => { return GameController.manager.doorMan.initFinish; }, () =>
            {
                DoorInfo info = GameController.manager.doorMan.GetDoorInfoById(id);
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
    }

    public bool CanEnter()
    {
        bool canEnter = GameController.manager.doorMan.GetDoorInfoById(id).CanEnter();
        return canEnter;
    }

    public int GetDestScene(int curRoomId)
    {
        DoorInfo info = GameController.manager.doorMan.GetDoorInfoById(id);
        return Util.roomToSceneDict[info.GetDestRoomId(curRoomId)];
    }

    public DoorInfo GetInfo()
    {
        return GameController.manager.doorMan.GetDoorInfoById(id);
    }
}