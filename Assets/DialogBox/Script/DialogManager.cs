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
    public int dialogId;
    public int id;
    public string text;
    public int index;
}

public class DialogManager
{
    public Dictionary<int, DialogInfo> dialogInfoDict = new Dictionary<int, DialogInfo>();

    // 第一个id是dialog的id 第二个id是每个选项的index 不能大于总的选项数量
    public Dictionary<int, Dictionary<int, Util.NoParmsCallBack>> selectionCallbackDict =
        new Dictionary<int, Dictionary<int, Util.NoParmsCallBack>>();

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
                for (int i = 0; i < nextDialogData.Count; i++)
                {
                    NextDialogInfo dialogToIndex = new NextDialogInfo();
                    dialogToIndex.dialogId = dialogInfo.id;
                    dialogToIndex.text = (string) nextDialogData[i]["text"];
                    dialogToIndex.id = (int) nextDialogData[i]["id"];
                    dialogToIndex.index = i;
                    dialogInfo.nextDialogList.Add(dialogToIndex);
                }
            }

            dialogInfoDict.Add(dialogInfo.id, dialogInfo);
        }

        Debug.Log("对话数据加载完成，一共加载了"+ dialogInfoDict.Count.ToString() + "个对话脚本");
    }

    public void RegisterSelectionCallback(int dialogId, int nextIndex, Util.NoParmsCallBack callback)
    {
        if (!dialogInfoDict.ContainsKey(dialogId))
        {
            Debug.LogError("Don't Exist id : " + dialogId);
            return;
        }

        if (nextIndex >= dialogInfoDict[dialogId].nextDialogList.Count || nextIndex < 0)
        {
            Debug.LogError("nextIndex is invalid");
            return;
        }

        if (!selectionCallbackDict.ContainsKey(dialogId))
        {
            selectionCallbackDict[dialogId] = new Dictionary<int, Util.NoParmsCallBack>();
        }

        selectionCallbackDict[dialogId][nextIndex] = callback;
    }

    public void CheckAndExecuteSelectionCallback(NextDialogInfo info)
    {
        if (!selectionCallbackDict.ContainsKey(info.dialogId))
        {
            Debug.LogError("no callback with id : " + info.dialogId);
            return;
        }

        if (!selectionCallbackDict[info.dialogId].ContainsKey(info.index))
        {
            Debug.LogError("Error index :  " + info.index);
            return;
        }
        selectionCallbackDict[info.dialogId][info.index]?.Invoke();
    }
}