using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorItem : MonoBehaviour
{
    public int id;
    public Transform showPosTrans;
    
    public bool CanEnter()
    {
        bool canEnter =  GameController.manager.doorMan.GetNormalInfoById(id).CanEnter();
        return canEnter;
    }
    
    public int GetDestScene()
    {
        NormalDoorInfo info = GameController.manager.doorMan.GetNormalInfoById(id);
        return Util.roomToSceneDict[info.toRoomId];
    }
    
    public NormalDoorInfo GetInfo()
    {
        return GameController.manager.doorMan.GetNormalInfoById(id);
    }
}