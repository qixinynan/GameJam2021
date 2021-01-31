using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterItem : MonoBehaviour
{
    public int id;
    public Transform showPosTrans;
    
    public int GetDestScene(int curRoomId)
    {
        EnterInfo info = GameController.manager.doorMan.GetEnterInfoById(id);
        return Util.roomToSceneDict[info.GetDestRoomId(curRoomId)];
    }

    public EnterInfo GetInfo()
    {
        EnterInfo info = GameController.manager.doorMan.GetEnterInfoById(id);
        return info;
    }
}
