using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class DataInfo
{
    public int id;
}

public class DataManager
{
    // list
    public List<DataInfo> dataList = new List<DataInfo>();
    // dict
    public Dictionary<int, DataInfo> dataDict = new Dictionary<int, DataInfo>();
    
    public void ParseData(JsonData data)
    {
        //...
    }
}
