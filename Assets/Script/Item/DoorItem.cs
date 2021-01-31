using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : MonoBehaviour
{
    public int id;
    public Transform showPosTrans;

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