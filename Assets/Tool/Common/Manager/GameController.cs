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
    public bool isGameOver = false;
    public GameObject player;
    public GameObject boy;
    public GameObject girl;
    public Vector2 boyPos;
    public Vector2 girlPos;
    public Vector2 bossPos;
    
    
    public bool isControllBoy;
    public ScreenFader screenFader;

    public CinemachineVirtualCamera virtualCamera;

    public int boyRoomId = 0;
    public int girlRoomId = 1;
    public int bossRoomId = 1;

    public int enterDoorId = -1;

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

    public void LoadLevel(int level, Util.NoParmsCallBack changeLevel = null, Util.NoParmsCallBack sameLevel = null)
    {
        GameController.manager.disableInput = true;
        screenFader.ScreenToBlack(() =>
        {
            if (level != SceneManager.GetActiveScene().buildIndex)
            {
                // set GameController Pos
                SceneManager.LoadScene(level);
                changeLevel?.Invoke();
            }
            else
            {
                // set CurrentPos
                sameLevel?.Invoke();
            }
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
            PlayerMovement.instance.UpdateAnim(boy, 0, 0);
        }
        else
        {
            girl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateAnim(girl, 0, 0);
        }
        isControllBoy = !isControllBoy;
        player = isControllBoy ? boy : girl;
        int id = isControllBoy ? boyRoomId : girlRoomId;
        if (player != null)
        {
            virtualCamera.Follow = isControllBoy ? boy.transform : girl.transform;
            if (isControllBoy)
            {
                // girl sleep
                girl.GetComponent<Animator>().SetInteger("dir", -1);
                girl.GetComponent<Animator>().SetTrigger("sleep");
            }
            else
            {
                // boy sleep
                boy.GetComponent<Animator>().SetInteger("dir", -1);
                boy.GetComponent<Animator>().SetTrigger("sleep");
            }
        }
        else if(Util.roomToSceneDict.ContainsKey(id))
        {
            if (isControllBoy)
            {
                // girl sleep
                girl.GetComponent<Animator>().SetInteger("dir", -1);
                girl.GetComponent<Animator>().SetTrigger("sleep");
            }
            else
            {
                // boy sleep
                boy.GetComponent<Animator>().SetInteger("dir", -1);
                boy.GetComponent<Animator>().SetTrigger("sleep");
            }
            LoadLevel(Util.roomToSceneDict[id]);
        }
        else
        {
            Debug.LogError("Error room id : " + id);
        }
    }
}
