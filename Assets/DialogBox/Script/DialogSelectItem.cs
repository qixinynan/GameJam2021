using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogSelectItem : MonoBehaviour, IPointerClickHandler
{
    private NextDialogInfo curInfo;
    public Text showText;

    public void SetContent(NextDialogInfo info)
    {
        curInfo = info;
        showText.text = info.text;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.dialogBoxPanel.ShowDialog(curInfo.id);
    }
}
