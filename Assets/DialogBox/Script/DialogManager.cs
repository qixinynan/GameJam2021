using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogBoxPenel dialog;
    public string jsonPath;
    // Start is called before the first frame update
    void Start()
    {
        init();
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
            dialogJsonData.nextDialogID = null; //TODO
                            
            Debug.Log(item["nextDialogID"].ToString());
        }
        
        
        
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
