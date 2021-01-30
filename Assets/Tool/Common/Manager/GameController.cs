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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeRole(); 
        }
    }

    public void LoadLevel(int level)
    {
        screenFader.ScreenToBlack(() =>
        {
            SceneManager.LoadScene(level);
            GameController.manager.disableInput = true;
            screenFader.ScreenToClear(() => { GameController.manager.disableInput = false; });
        });
    }

    public void ChangeRole()
    {
        if (isControllBoy)
        {
            boy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateAnim(boy, 0, 0);
        }
        else
        {
            girl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateAnim(girl, 0, 0);
        }
        isControllBoy = !isControllBoy;
        player = isControllBoy ? boy : girl;
        virtualCamera.Follow = isControllBoy ? boy.transform : girl.transform;
    }
}
