using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using LitJson;
using NUnit.Framework;
using Debug = UnityEngine.Debug;

public enum ColorType
{
    Red = 0,
    Green,
    Blue
}


public class DoorInfo
{
    public int id;
    public int switchId;
    public ColorType colorType;
    public List<int> roomList = new List<int>();

    public bool CanEnter()
    {
        bool sameControl = false;
        SwitchInfo switchInfo = GameController.manager.doorMan.GetSwitchInfoById(switchId);
        if (switchInfo == null)
        {
            return false;
        }

        return switchInfo.colorType == colorType;
    }
    
    public int GetDestRoomId(int roomId)
    {
        Debug.Log(roomId + "-------");
        int des = 0;
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i] != roomId)
            {
                des = i;
                Debug.Log("Find !!!!!");
                break;
            }
        }

        return roomList[des];
    }
}

public class SwitchInfo
{
    public int id;
    public ColorType colorType;

    public void SwitchType()
    {
        int t = (int) colorType + 1;
        t %= 3;
        colorType = (ColorType) t;
        Debug.Log("current type is : " + colorType);
    }
}

public class EnterInfo
{
    public int id;
    public List<int> roomList = new List<int>();

    public int GetDestRoomId(int roomId)
    {
        Debug.Log(roomId + "-------");
        int des = 0;
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i] != roomId)
            {
                des = i;
                Debug.Log("Find !!!!!");
                break;
            }
        }

        return roomList[des];
    }
}

public class DoorMan
{
    public Dictionary<int, DoorInfo> doorInfoDict = new Dictionary<int, DoorInfo>();
    public Dictionary<int, SwitchInfo> switchInfoDict = new Dictionary<int, SwitchInfo>();
    public Dictionary<int, EnterInfo> enterInfoDict = new Dictionary<int, EnterInfo>();

    public bool initFinish = false;
    public void ParseDoorInfos()
    {
        JsonData controlData = JsonMapper.ToObject(Resources.Load<TextAsset>("DoorData/doors").text);
        foreach (JsonData item in controlData)
        {
            DoorInfo doorInfo = new DoorInfo();
            doorInfo.id = (int) item["id"];
            doorInfo.switchId = (int) item["switchId"];
            doorInfo.colorType = (ColorType) (int) item["type"];
            JsonData dt = item["rooms"];
            for (int i = 0; i < dt.Count; i++)
            {
                doorInfo.roomList.Add((int) dt[i]);
            }
            doorInfoDict[doorInfo.id] = doorInfo;
        }

        JsonData normalData = JsonMapper.ToObject(Resources.Load<TextAsset>("DoorData/switches").text);
        foreach (JsonData item in normalData)
        {
            SwitchInfo switchInfo = new SwitchInfo();
            switchInfo.id = (int) item["id"];
            switchInfo.colorType = (ColorType) (int) item["type"];
            switchInfoDict[switchInfo.id] = switchInfo;
        }
        
        JsonData enterData = JsonMapper.ToObject(Resources.Load<TextAsset>("DoorData/enters").text);
        foreach (JsonData item in enterData)
        {
            EnterInfo enterInfo = new EnterInfo();
            enterInfo.id = (int) item["id"];
            JsonData dt = item["rooms"];
            for (int i = 0; i < dt.Count; i++)
            {
                enterInfo.roomList.Add((int) dt[i]);
            }
            enterInfoDict[enterInfo.id] = enterInfo;
        }

        initFinish = true;
    }

    public DoorInfo GetDoorInfoById(int id)
    {
        if (!doorInfoDict.ContainsKey(id))
        {
            return null;
        }

        return doorInfoDict[id];
    }

    public SwitchInfo GetSwitchInfoById(int id)
    {
        if (!switchInfoDict.ContainsKey(id))
        {
            return null;
        }

        return switchInfoDict[id];
    }
    
    public EnterInfo GetEnterInfoById(int id)
    {
        if (!enterInfoDict.ContainsKey(id))
        {
            return null;
        }

        return enterInfoDict[id];
    }
}
