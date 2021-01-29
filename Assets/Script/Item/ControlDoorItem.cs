using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDoorItem : MonoBehaviour
{
    public int id;
    
    public void SwitchType()
    {
        ControlDoorInfo info = GameController.manager.doorMan.GetControlInfoById(id);
        if (info != null)
        {
            info.SwitchType();
        }
    }

}