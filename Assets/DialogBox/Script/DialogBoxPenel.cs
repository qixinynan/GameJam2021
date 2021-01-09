using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxPenel : MonoBehaviour
{
    public Text showText;
    public Text nameText;
    public Image faceImage;
    public Button nextBtn;

    public List<DialogData> dialogData_List = new List<DialogData>();

    void Start()
    {
        Init();
    }

    void Init()
    {
        nextBtn.onClick.AddListener(() =>
        {
            if (dialogData_List.Count > 0)
            {
                DialogData data = dialogData_List[0];
                SetDialogData(data.text, data.name, data.faceImage);
                dialogData_List.RemoveAt(0);
            }
            else
            {
                gameObject.SetActive(false);
            }
        });
    }

    void SetDialogData(string _text, string _name, Sprite _faceImage)
    {
        showText.text = _text;
        nameText.text = _name;
        faceImage.sprite = _faceImage;
    }

    public void ShowDialog(string _text,string _name,Sprite _faceImage)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            SetDialogData(_text, _name, _faceImage);
        }
        else
        {
            dialogData_List.Add(new DialogData(_text,_name,_faceImage));
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
