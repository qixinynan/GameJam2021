using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public enum DoorColorType
{
    Red = 0,
    Green,
    Blue
}

public enum DoorType
{
    Normal = 0,
    Control
}

public class ControlDoorInfo
{
    public int id;
    public DoorColorType colorType;

    public void SwitchType()
    {
        int t = ((int)colorType + 1) % 3;
        colorType = (DoorColorType) t;
        Debug.Log("current type is :  " + colorType);
    }
}

public class NormalDoorInfo
{
    public int id;
    public int parentId;
    public DoorColorType colorType;
    public int toRoomId;

    public bool CanEnter()
    {
        ControlDoorInfo info = GameController.manager.doorMan.GetControlInfoById(parentId);
        if (info == null)
        {
            return false;
        }

        return info.colorType == colorType;
    }
    
}

public class DoorMan
{
    public Dictionary<int, ControlDoorInfo> controlDoorDict = new Dictionary<int, ControlDoorInfo>();
    public Dictionary<int, NormalDoorInfo> normalDoorDict = new Dictionary<int, NormalDoorInfo>();

    public void ParseDoorInfos()
    {
        JsonData controlData = JsonMapper.ToObject(Resources.Load<TextAsset>("DoorData/controlDoors").text);
        foreach (JsonData item in controlData)
        {
            ControlDoorInfo controlDoorInfo = new ControlDoorInfo();
            controlDoorInfo.id = (int) item["id"];
            controlDoorInfo.colorType = (DoorColorType) (int)item["type"];
            controlDoorDict[controlDoorInfo.id] = controlDoorInfo;
        }
        JsonData normalData = JsonMapper.ToObject(Resources.Load<TextAsset>("DoorData/normalDoors").text);
        foreach (JsonData item in normalData)
        {
            NormalDoorInfo normalDoorInfo = new NormalDoorInfo();
            normalDoorInfo.id = (int) item["id"];
            normalDoorInfo.parentId = (int) item["parentId"];
            normalDoorInfo.colorType = (DoorColorType) (int)item["type"];
            normalDoorInfo.toRoomId = (int) item["toRoomId"];
            normalDoorDict[normalDoorInfo.id] = normalDoorInfo;
        }
    }

    public ControlDoorInfo GetControlInfoById(int id)  
    {
        if (!controlDoorDict.ContainsKey(id))
        {
            return null;
        }

        return controlDoorDict[id];
    }
    
    public NormalDoorInfo GetNormalInfoById(int id)  
    {
        if (!normalDoorDict.ContainsKey(id))
        {
            return null;
        }

        return normalDoorDict[id];
    }
}
