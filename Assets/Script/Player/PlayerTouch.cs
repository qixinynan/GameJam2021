using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    private bool isTouchDoor = false;
    private GameObject selectDoorItem;

    private void Update()
    {
        if (isTouchDoor && selectDoorItem != null)
        {
            if (selectDoorItem.GetComponent<ControlDoorItem>() != null && Input.GetKeyDown(KeyCode.E))
            {
                selectDoorItem.GetComponent<ControlDoorItem>().SwitchType();
            }
            else if (selectDoorItem.GetComponent<NormalDoorItem>() != null &&
                     selectDoorItem.GetComponent<NormalDoorItem>().CanEnter())
            {
                // judge and enter next room 
            
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Util.TagCollection.door))
        {
            selectDoorItem = other.gameObject;
            isTouchDoor = true;
            Debug.Log("交互门");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Util.TagCollection.door))
        {
            selectDoorItem = null;
            isTouchDoor = false;
            Debug.Log("取消交互门");
        }
    }
}
