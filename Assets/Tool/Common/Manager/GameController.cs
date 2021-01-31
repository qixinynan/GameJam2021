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
    public bool disableSpace = true;
    
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
    public GameObject thanksBg;



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
        }
        if (screenFader.isAnimanating)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Space) && !GameController.manager.disableSpace)
        {
            ChangeRole();
        }
    }

    public void LoadLevel(int level, Util.NoParmsCallBack changeLevel = null)
    {
        Debug.Log("-=--==--==-=-=-=-=-=-");
        Debug.Log("LoadLevel.......");
        GameController.manager.disableInput = true;
        screenFader.ScreenToBlack(() =>
        {
            if (level != SceneManager.GetActiveScene().buildIndex)
            {
                // set GameController Pos
                Debug.Log(level);
                if (changeLevel != null)
                {
                    changeLevel();
                }
                SceneManager.LoadScene(level);
            }
            screenFader.ScreenToClear(() =>
            {
                GameController.manager.disableInput = false;
            });
        });
    }

    public void ChangeRole()
    {
        GameController.manager.disableInput = true;
        if (isControllBoy)
        {
            boy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateMove(boy, 0, 0);
            boyPos = GameController.manager.boy.transform.position;
        }
        else
        {
            girl.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.instance.UpdateMove(girl, 0, 0);
            girlPos = GameController.manager.girl.transform.position;
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
                boy.GetComponent<Animator>().SetInteger("dir", 0);
                girl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                boy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                // boy sleep
                boy.GetComponent<Animator>().SetInteger("dir", -1);
                boy.GetComponent<Animator>().SetTrigger("sleep");
                girl.GetComponent<Animator>().SetInteger("dir", 0);
                boy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                girl.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

            //StartCoroutine(ResetInput());
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
            Debug.Log("000000000000000");
            LoadLevel(Util.roomToSceneDict[id]);
        }
        else
        {
            Debug.LogError("Error room id : " + id);
        }
    }

    // private IEnumerator ResetInput()
    // {
    //     yield return new WaitForSeconds(3.0f);
    //     GameController.manager.disableInput = false;
    // }
}
