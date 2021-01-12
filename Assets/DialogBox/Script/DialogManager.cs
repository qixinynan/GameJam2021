using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfo
{
    public int id;
    public string text;
    public string name;
    public Sprite faceSprite;
    public List<NextDialogInfo> nextDialogList;
}

public class NextDialogInfo
{
    public string text;
    public int id;
}

public class DialogManager
{
    public Dictionary<int, DialogInfo> dialogInfoDict = new Dictionary<int, DialogInfo>();
    public DialogInfo currentInfo;
    
    public void ParseDialogInfo()
    {
        JsonData jsonData = JsonMapper.ToObject(Resources.Load<TextAsset>("DialogData/DialogTest01").text);
        foreach (JsonData item in jsonData)
        {
            DialogInfo dialogInfo = new DialogInfo();
            dialogInfo.id = (int) item["id"];
            dialogInfo.name = item["name"].ToString();
            dialogInfo.text = item["text"].ToString();

            if ((string) item["faceImagePath"] != "")
            {
                string iconPath = (string) item["faceImagePath"];
                dialogInfo.faceSprite = Resources.Load<Sprite>(iconPath);
            }

            dialogInfo.nextDialogList = new List<NextDialogInfo>(); //TODO
            var nextDialogData = item["nextDialog"];
            if (nextDialogData.Count > 0)
            {
                foreach (JsonData item1 in nextDialogData)
                {
                    NextDialogInfo dialogToIndex = new NextDialogInfo();
                    dialogToIndex.text = (string) item1["text"];
                    dialogToIndex.id = (int) item1["id"];
                    dialogInfo.nextDialogList.Add(dialogToIndex);
                }
            }
            dialogInfoDict.Add(dialogInfo.id, dialogInfo);
        }

        Debug.Log("对话数据加载完成，一共加载了"+ dialogInfoDict.Count.ToString() + "个对话脚本");
    }
}