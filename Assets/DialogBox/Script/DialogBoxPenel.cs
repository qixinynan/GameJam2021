using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxPenel : MonoBehaviour
{
    public GameObject DialogPanel;
    public Text textBox;
    public Text nameTextBox;
    public Image faceImageBox;
    public Button nextButton;

    public List<DialogData> dialogData_List = new List<DialogData>();

    void Start()
    {
        Init();
        ShowDialog("Hello", "Name", null);   
        ShowDialog("Hello1", "Name2", null);   
        ShowDialog("Hello4", "Name3", null);   
    }
    void Update()
    {
        
    }

    void Init()
    {
        nextButton.onClick.AddListener(NextText);
    }

    void SetDialogData(string _text, string _name, Sprite _faceImage)
    {
        textBox.text = _text;
        nameTextBox.text = _name;
        faceImageBox.sprite = _faceImage;
    }

    void NextText()
    {
        if(dialogData_List.Count == 0)
        {
            DialogPanel.SetActive(false);
        }
        else
        {
            DialogData data = dialogData_List[0];
            SetDialogData(data.text,data.name,data.faceImage);
            dialogData_List.RemoveAt(0);
        }
    }

    public void ShowDialog(string _text,string _name,Sprite _faceImage)
    {

        if (DialogPanel.activeSelf)
        {
            DialogData dialogData = new DialogData(_text, _name, _faceImage);
            dialogData_List.Add(dialogData);
        }
        else
        {
            
            DialogPanel.SetActive(true);
            SetDialogData(_text, _name, _faceImage);
        }

    }
}


public class DialogData
{
    public string text;
    public string name;
    public Sprite faceImage;
    public DialogData(string _text,string _name,Sprite _faceImage)
    {
        text = _text;
        name = _name;
        faceImage = _faceImage;
    }
}
