using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxPanel : MonoBehaviour
{
    public Text showText;
    public Text nameText;
    public Image faceImage;

    public Transform selectItemParent;
    public DialogSelectItem selectItemPrefab;

    private void Start()
    {
        ShowDialog(0);
    }

    public void ShowDialog(int id)
    {
        if (id < 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        DialogInfo info = GameController.manager.dialogMan.dialogInfoDict[id];
        showText.text = info.text;
        nameText.text = info.name;
        faceImage.sprite = info.faceSprite;
        // 根据情况显示所有选项按钮
        Util.DeleteChildren(selectItemParent);
        for (int i = 0; i < info.nextDialogList.Count; i++)
        {
            DialogSelectItem item = Instantiate(selectItemPrefab);
            item.SetContent(info.nextDialogList[i]);
            item.transform.SetParent(selectItemParent, false);
        }
    }
}
