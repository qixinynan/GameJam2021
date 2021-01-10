using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogBoxPenel dialog;
    public string jsonPath;

    //public List<DialogJsonData> dialogJsonData_List = new List<DialogJsonData>();
    public Dictionary<int, DialogJsonData> dialogJsonData_Dic = new Dictionary<int, DialogJsonData>();
    // Start is called before the first frame update
    void Start()
    {
        init();
        PlayDialog(0);
      //dialog.ShowDialog("Hello", "Name", null);
      //dialog.ShowDialog("Hello1", "Name2", null);
      //dialog.ShowDialog("Hello4", "Name3", null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void init()
    {
        JsonData jsonData = JsonMapper.ToObject(Resources.Load<TextAsset>("DialogData/DialogTest01").text);
        foreach (JsonData item in jsonData)
        {
            DialogJsonData dialogJsonData= new DialogJsonData();
            dialogJsonData.id = (int)item["id"];
            dialogJsonData.name = item["name"].ToString();
            dialogJsonData.text = item["text"].ToString();

            if (item["faceImagePath"] != null)
            {
                string iconPath = item["faceImagePath"].ToString();
                dialogJsonData.faceImage = Resources.Load<Sprite>(iconPath);
            }
            dialogJsonData.nextDialogID = new List<int>(); //TODO
            if (item["nextDialogID"] != null)
            {
                foreach (JsonData item1 in item["nextDialogID"])
                {
                    dialogJsonData.nextDialogID.Add((int)item1);
                }
                
            }
            dialogJsonData_Dic.Add(dialogJsonData.id,dialogJsonData);
        }

        Debug.Log("对话数据加载完成，一共加载了"+ dialogJsonData_Dic.Count.ToString() + "个对话脚本");
        
    }
    public void PlayDialog(int id)
    {
        DialogJsonData jsonDialog = dialogJsonData_Dic[id];
        dialog.ShowDialog(DialogJsonDataToDialogData(jsonDialog));
        if(jsonDialog.nextDialogID.Count == 0)
        {
            return;
        }
        else if(jsonDialog.nextDialogID.Count >= 2)
        {
            //TODO
            Debug.LogError("多分支对话还未完成,目前自动播放第一号对话");
            PlayDialog(jsonDialog.nextDialogID[0]);
        }
        else
        {
            PlayDialog(jsonDialog.nextDialogID[0]);
        }
    }
    public DialogData DialogJsonDataToDialogData(DialogJsonData jsonData)
    {
        DialogData data = new DialogData(jsonData.text,jsonData.name,jsonData.faceImage);
        return data;

    }
}

public class DialogJsonData
{
    public int id { get; set; }
    public string text{ get; set; }
    public string name { get; set; }
    public Sprite faceImage{ get; set; }
    public List<int> nextDialogID { get; set; }
    public List<string> branchTitle { get; set; }
}
