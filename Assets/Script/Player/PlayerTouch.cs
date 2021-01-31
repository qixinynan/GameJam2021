using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchingType
{
    None = 0,
    Door,
    Switch,
    Enter
}

public class PlayerTouch : MonoBehaviour
{
    private TouchingType touchingType = TouchingType.None;
    private GameObject selectItem;
    public bool isBoy;
 
    private void Update()
    {
        if (GameController.manager.disableInput)
        {
            return;
        }

        if (GameController.manager.isControllBoy != isBoy)
        {
            return;
        }
        if (selectItem != null)
        {
            if (touchingType == TouchingType.Door)
            {
                if (Input.GetKeyDown(KeyCode.E) && selectItem.GetComponent<DoorItem>().CanEnter() )
                {
                    Debug.Log("11111111111111111");
                    GameController.manager.LoadLevel(selectItem.GetComponent<DoorItem>().GetDestScene(PlayerIniter.instance.roomId),
                        () =>
                        {
                            // TODO
                            Debug.Log("Change Scene");
                            GameController.manager.enterDoorId = selectItem.GetComponent<DoorItem>().GetInfo().id;
                            int rid = selectItem.GetComponent<DoorItem>().GetInfo().GetDestRoomId(PlayerIniter.instance.roomId);
                            if (GameController.manager.isControllBoy)
                            {
                                GameController.manager.boyRoomId = rid;
                            }
                            else
                            {
                                GameController.manager.girlRoomId = rid;
                            }
                        });
                }
            } else if (touchingType == TouchingType.Switch)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectItem.GetComponent<SwitchItem>().SwitchType();
                }
            } else if (touchingType == TouchingType.Enter)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameController.manager.LoadLevel(
                        selectItem.GetComponent<EnterItem>().GetDestScene(PlayerIniter.instance.roomId),
                        () =>
                        {
                            // TODO
                            Debug.Log("Change Scene");
                            GameController.manager.enterDoorId = selectItem.GetComponent<EnterItem>().GetInfo().id;
                            int rid = selectItem.GetComponent<EnterItem>().GetInfo()
                                .GetDestRoomId(PlayerIniter.instance.roomId);
                            if (GameController.manager.isControllBoy)
                            {
                                GameController.manager.boyRoomId = rid;
                            }
                            else
                            {
                                GameController.manager.girlRoomId = rid;
                            }
                        });
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Util.TagCollection.door))
        {
            selectItem = other.gameObject;
            touchingType  = TouchingType.Door;
            Debug.Log("交互 门");
        } else if (other.gameObject.CompareTag(Util.TagCollection.switches))
        {
            selectItem = other.gameObject;
            touchingType  = TouchingType.Switch;
            Debug.Log("交互 开关");
        }  else if (other.gameObject.CompareTag(Util.TagCollection.enter))
        {
            selectItem = other.gameObject;
            touchingType  = TouchingType.Enter;
            Debug.Log("交互 出口");
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Util.TagCollection.door) 
            || other.gameObject.CompareTag(Util.TagCollection.switches)
            || other.gameObject.CompareTag(Util.TagCollection.enter))
        {
            selectItem = null;
            touchingType = TouchingType.None;
            Debug.Log("取消交互");
        }
    }
}
