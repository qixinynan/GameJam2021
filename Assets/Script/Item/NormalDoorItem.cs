using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorItem : MonoBehaviour
{
    public int id;
    
    public bool CanEnter()
    {
        bool canEnter =  GameController.manager.doorMan.GetNormalInfoById(id).CanEnter();
        return canEnter;
    }
}