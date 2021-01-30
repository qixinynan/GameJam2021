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
            if (selectDoorItem.GetComponent<ControlDoorItem>() != null)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    selectDoorItem.GetComponent<ControlDoorItem>().SwitchType();
                }  else if (Input.GetKeyDown(KeyCode.E) && selectDoorItem.GetComponent<ControlDoorItem>().CanEnter())
                {
                    // judge and enter next room 
                    Debug.Log("Enter next room");
                    GameController.manager.LoadLevel(selectDoorItem.GetComponent<ControlDoorItem>().GetDestScene(),
                        () =>
                        {
                            // TODO
                            Debug.Log("Change Scene");
                            GameController.manager.enterDoorId =
                                selectDoorItem.GetComponent<ControlDoorItem>().GetInfo().backDoorId;
                        }, () =>
                        {
                            Vector3 pos = Vector3.zero;
                            ControlDoorItem[] controlItems = FindObjectsOfType<ControlDoorItem>();
                            NormalDoorItem[] normalItems = FindObjectsOfType<NormalDoorItem>();
                            bool find = false;
                            for (int i = 0; i < controlItems.Length; i++)
                            {
                                if (controlItems[i].id == selectDoorItem.GetComponent<ControlDoorItem>().GetInfo().backDoorId)
                                {
                                    GameController.manager.player.transform.position =
                                        controlItems[i].showPosTrans.position;
                                    return;
                                }
                            }
                            for (int i = 0; i < normalItems.Length; i++)
                            {
                                if (normalItems[i].id == selectDoorItem.GetComponent<ControlDoorItem>().GetInfo().backDoorId)
                                {
                                    GameController.manager.player.transform.position =
                                        controlItems[i].showPosTrans.position;
                                    return;
                                }
                            }
                        });
                }
            }
            else if (selectDoorItem.GetComponent<NormalDoorItem>() != null &&
                     selectDoorItem.GetComponent<NormalDoorItem>().CanEnter() && Input.GetKeyDown(KeyCode.E))
            {
                // judge and enter next room 
                Debug.Log("Enter next room");
                GameController.manager.LoadLevel(selectDoorItem.GetComponent<NormalDoorItem>().GetDestScene(),
                    () =>
                    {
                        // TODO
                        Debug.Log("Change Scene");
                        GameController.manager.enterDoorId =
                            selectDoorItem.GetComponent<ControlDoorItem>().GetInfo().backDoorId;
                    }, () =>
                    {
                        Vector3 pos = Vector3.zero;
                        ControlDoorItem[] controlItems = FindObjectsOfType<ControlDoorItem>();
                        NormalDoorItem[] normalItems = FindObjectsOfType<NormalDoorItem>();
                        bool find = false;
                        for (int i = 0; i < controlItems.Length; i++)
                        {
                            if (controlItems[i].id == selectDoorItem.GetComponent<NormalDoorItem>().GetInfo().backDoorId)
                            {
                                GameController.manager.player.transform.position =
                                    controlItems[i].showPosTrans.position;
                                return;
                            }
                        }
                        for (int i = 0; i < normalItems.Length; i++)
                        {
                            if (normalItems[i].id == selectDoorItem.GetComponent<ControlDoorItem>().GetInfo().backDoorId)
                            {
                                GameController.manager.player.transform.position =
                                    controlItems[i].showPosTrans.position;
                                return;
                            }
                        }
                    });
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
