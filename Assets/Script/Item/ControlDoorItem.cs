using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDoorItem : MonoBehaviour
{
    public int id;
    public Transform showPosTrans;
    
    public void SwitchType()
    {
        ControlDoorInfo info = GameController.manager.doorMan.GetControlInfoById(id);
        if (info != null)
        {
            info.SwitchType();
        }
    }
    
    public bool CanEnter()
    {
        bool canEnter =  GameController.manager.doorMan.GetControlInfoById(id).CanEnter();
        return canEnter;
    }

    public int GetDestScene()
    {
        ControlDoorInfo info = GameController.manager.doorMan.GetControlInfoById(id);
        return Util.roomToSceneDict[info.toRoomId];
    }

    public ControlDoorInfo GetInfo()
    {
        return GameController.manager.doorMan.GetControlInfoById(id);
    }
}