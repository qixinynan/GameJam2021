using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerIniter : MonoBehaviour
{
    public GameObject boyPrefab;
    public GameObject girlPrefab;
    public GameObject bossPrefab;

    public static PlayerIniter instance;
    
    public int roomId;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() =>
        {
            return GameController.manager != null;
        });
        GameController.manager.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        // init player
        GameController.manager.player = null;
        GameController.manager.boy = null;
        GameController.manager.girl = null;

        Vector3 startPos = Vector3.zero;
        if (GameController.manager.enterDoorId != -999 && FindStartPos(out startPos))
        {
            if (GameController.manager.isControllBoy)
            {
                GameController.manager.boyPos = startPos;
            }
            else
            {
                GameController.manager.girlPos = startPos;
            }
        }

        if (GameController.manager.boyRoomId == roomId)
        {
            GameObject boy = Instantiate(boyPrefab);
            boy.transform.position = GameController.manager.boyPos;
            if (GameController.manager.isControllBoy)
            {
                GameController.manager.player = boy;
                GameController.manager.virtualCamera.Follow = boy.transform;
            }

            GameController.manager.boy = boy;
        }
        if (GameController.manager.girlRoomId == roomId)
        {
            GameObject girl = Instantiate(girlPrefab);
            girl.transform.position = GameController.manager.girlPos;
            if (!GameController.manager.isControllBoy)
            {
                GameController.manager.player = girl;
                GameController.manager.virtualCamera.Follow = girl.transform;
            }

            GameController.manager.girl = girl;
        }
        if (GameController.manager.bossRoomId == roomId)
        {
            GameObject boss = Instantiate(bossPrefab);
            bossPrefab.transform.position = GameController.manager.bossPos;
        }

        if (!GameController.manager.isControllBoy)
        {
            GameController.manager.player.GetComponent<Animator>().SetInteger("dir",0);
            GameController.manager.player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

    }

    private bool FindStartPos(out Vector3 pos)
    {
        DoorItem[] doorItems = FindObjectsOfType<DoorItem>();
        for (int i = 0; i < doorItems.Length; i++)
        {
            if (doorItems[i].id == GameController.manager.enterDoorId)
            {
                pos = doorItems[i].showPosTrans.position;
                return true;
            }
        }
        
        EnterItem[] enterItems = FindObjectsOfType<EnterItem>();
        for (int i = 0; i < enterItems.Length; i++)
        {
            if (enterItems[i].id == GameController.manager.enterDoorId)
            {
                pos = enterItems[i].showPosTrans.position;
                return true;
            }
        }
        
        pos = Vector3.zero;
        return false;
    }
}
