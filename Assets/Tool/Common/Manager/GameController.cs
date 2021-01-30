using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController manager = null;
    public DialogManager dialogMan = new DialogManager();
    public TaskManager taskMan = new TaskManager();
    public DoorMan doorMan = new DoorMan();

    public bool disableInput = false;
    public GameObject player;
    public GameObject boy;
    public GameObject girl;
    public Vector2 boyPos;
    public Vector2 girlPos;
    public bool isControllBoy;
    public ScreenFader screenFader;

    public CinemachineVirtualCamera virtualCamera;

    public int boyRoomId = 0;
    public int girlRoomId = 1;


    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogMan.ParseDialogInfo();
        taskMan.ParseTaskInfo();
        doorMan.ParseDoorInfos();
    }

    private void Update()
    {
        if (disableInput)
        {
            return;
            ;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeRole(); 
        }
    }

    public void LoadLevel(int level)
    {
        GameController.manager.disableInput = true;
        screenFader.ScreenToBlack(() =>
        {
            SceneManager.LoadScene(level);
            screenFader.ScreenToClear(() =>
            {
                GameController.manager.disableInput = false;
            });
        });
    }

    public void ChangeRole()
    {
        if (isControllBoy)
        {
            boy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateMove(boy, 0, 0);
        }
        else
        {
            girl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateMove(girl, 0, 0);
        }
        isControllBoy = !isControllBoy;
        player = isControllBoy ? boy : girl;
        int id = isControllBoy ? boyRoomId : girlRoomId;
        if (player != null)
        {
            virtualCamera.Follow = isControllBoy ? boy.transform : girl.transform;
        }
        else if(Util.roomToSceneDict.ContainsKey(id))
        {
            LoadLevel(Util.roomToSceneDict[id]);
        }
        else
        {
            Debug.LogError("Error room id : " + id);
        }
    }
}
